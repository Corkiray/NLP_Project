package org.maltparser.parser;


import java.util.regex.Pattern;

import org.maltparser.core.exception.MaltChainedException;
import org.maltparser.core.helper.HashMap;
import org.maltparser.core.propagation.PropagationManager;
import org.maltparser.core.symbol.SymbolTable;
import org.maltparser.core.symbol.SymbolTableHandler;
import org.maltparser.core.symbol.Table;
import org.maltparser.core.symbol.TableHandler;
import org.maltparser.core.syntaxgraph.DependencyStructure;
import org.maltparser.core.syntaxgraph.LabelSet;
import org.maltparser.core.syntaxgraph.edge.Edge;
import org.maltparser.parser.history.GuideUserHistory;
import org.maltparser.parser.history.action.GuideUserAction;
import org.maltparser.parser.history.container.ActionContainer;
import org.maltparser.parser.transition.TransitionTable;
import org.maltparser.parser.transition.TransitionTableHandler;
/**
 * @author Johan Hall
 *
 */
public abstract class TransitionSystem {
	public final static Pattern decisionSettingsSplitPattern = Pattern.compile(",|#|;|\\+");
	private final HashMap<String, TableHandler> tableHandlers;
	private final PropagationManager propagationManager;
	protected final TransitionTableHandler transitionTableHandler;
	protected ActionContainer[] actionContainers;
	protected ActionContainer transActionContainer;
	protected ActionContainer[] arcLabelActionContainers;
	
	
	public TransitionSystem(PropagationManager _propagationManager) throws MaltChainedException {	
		this.transitionTableHandler = new TransitionTableHandler();
		this.tableHandlers = new HashMap<String, TableHandler>();
		this.propagationManager = _propagationManager;
	}
	
	public abstract void apply(GuideUserAction currentAction, ParserConfiguration config) throws MaltChainedException;
	public abstract boolean permissible(GuideUserAction currentAction, ParserConfiguration config) throws MaltChainedException;
	public abstract GuideUserAction getDeterministicAction(GuideUserHistory history, ParserConfiguration config) throws MaltChainedException;
	protected abstract void addAvailableTransitionToTable(TransitionTable ttable) throws MaltChainedException;
	protected abstract void initWithDefaultTransitions(GuideUserHistory history) throws MaltChainedException;
	public abstract String getName();
	public abstract GuideUserAction defaultAction(GuideUserHistory history, ParserConfiguration configuration) throws MaltChainedException;
	
	protected GuideUserAction updateActionContainers(GuideUserHistory history, int transition, LabelSet arcLabels) throws MaltChainedException {	
		transActionContainer.setAction(transition);

		if (arcLabels == null) {
			for (int i = 0; i < arcLabelActionContainers.length; i++) {
				arcLabelActionContainers[i].setAction(-1);	
			}
		} else {
			for (int i = 0; i < arcLabelActionContainers.length; i++) {
				if (arcLabelActionContainers[i] == null) {
					throw new MaltChainedException("arcLabelActionContainer " + i + " is null when doing transition " + transition);
				}
				
				Integer code = arcLabels.get(arcLabelActionContainers[i].getTable());
				if (code != null) {
					arcLabelActionContainers[i].setAction(code.shortValue());
				} else {
					arcLabelActionContainers[i].setAction(-1);
				}
			}		
		}
		GuideUserAction oracleAction = history.getEmptyGuideUserAction();
		oracleAction.addAction(actionContainers);
		return oracleAction;
	}
	
	protected boolean isActionContainersLabeled() {
		for (int i = 0; i < arcLabelActionContainers.length; i++) {
			if (arcLabelActionContainers[i].getActionCode() < 0) {
				return false;
			}
		}
		return true;
	}
	
	protected void addEdgeLabels(Edge e) throws MaltChainedException {
		if (e != null) { 
			for (int i = 0; i < arcLabelActionContainers.length; i++) {
				if (arcLabelActionContainers[i].getActionCode() != -1) {
					e.addLabel((SymbolTable)arcLabelActionContainers[i].getTable(), arcLabelActionContainers[i].getActionCode());
				} else {
					e.addLabel((SymbolTable)arcLabelActionContainers[i].getTable(), ((DependencyStructure)e.getBelongsToGraph()).getDefaultRootEdgeLabelCode((SymbolTable)arcLabelActionContainers[i].getTable()));
				}
			}
			if (propagationManager != null) {
				propagationManager.propagate(e);
			}
		}
	}
	
	public void initTransitionSystem(GuideUserHistory history) throws MaltChainedException {
		this.actionContainers = history.getActionContainerArray();
		if (actionContainers.length < 1) {
			throw new ParsingException("Problem when initialize the history (sequence of actions). There are no action containers. ");
		}
		int nLabels = 0;
		for (int i = 0; i < actionContainers.length; i++) {
			if (actionContainers[i].getTableContainerName().startsWith("A.")) {
				nLabels++;
			}
		}
		int j = 0;
		for (int i = 0; i < actionContainers.length; i++) {
			if (actionContainers[i].getTableContainerName().equals("T.TRANS")) {
				transActionContainer = actionContainers[i];
			} else if (actionContainers[i].getTableContainerName().startsWith("A.")) {
				if (arcLabelActionContainers == null) {
					arcLabelActionContainers = new ActionContainer[nLabels];
				}
				arcLabelActionContainers[j++] = actionContainers[i];
			}
		}
		initWithDefaultTransitions(history);
	}
	
	public void initTableHandlers(String decisionSettings, SymbolTableHandler symbolTableHandler) throws MaltChainedException {
		if (decisionSettings.equals("T.TRANS+A.DEPREL") || decisionSettings.equals("T.TRANS#A.DEPREL") || decisionSettings.equals("T.TRANS,A.DEPREL") || decisionSettings.equals("T.TRANS;A.DEPREL")) {
			tableHandlers.put("T", transitionTableHandler);
			addAvailableTransitionToTable((TransitionTable)transitionTableHandler.addSymbolTable("TRANS"));
			tableHandlers.put("A", symbolTableHandler);
			return;
		}
		initTableHandlers(decisionSettingsSplitPattern.split(decisionSettings), decisionSettings, symbolTableHandler);
	}
	
	public void initTableHandlers(String[] decisionElements, String decisionSettings, SymbolTableHandler symbolTableHandler) throws MaltChainedException {
		int nTrans = 0;
		for (int i = 0; i < decisionElements.length; i++) {
			int index = decisionElements[i].indexOf('.');
			if (index == -1) {
				throw new ParsingException("Decision settings '"+decisionSettings+"' contain an item '"+decisionElements[i]+"' that does not follow the format {TableHandler}.{Table}. ");
			}
			if (decisionElements[i].substring(0,index).equals("T")) {
				if (!tableHandlers.containsKey("T")) {
					tableHandlers.put("T", transitionTableHandler);
				}
				if (decisionElements[i].substring(index+1).equals("TRANS")) {
					if (nTrans == 0) {
						addAvailableTransitionToTable((TransitionTable)transitionTableHandler.addSymbolTable("TRANS"));
					} else {
						throw new ParsingException("Illegal decision settings '"+decisionSettings+"'");
					}
					nTrans++;
				}  
			} else if (decisionElements[i].substring(0,index).equals("A")) {
				if (!tableHandlers.containsKey("A")) {
					tableHandlers.put("A", symbolTableHandler);
				}
			} else {
				throw new ParsingException("The decision settings '"+decisionSettings+"' contains an unknown table handler '"+decisionElements[i].substring(0,index)+"'. " +
						"Only T (Transition table handler) and A (ArcLabel table handler) is allowed. ");
			}
		}
	}
	
	public void copyAction(GuideUserAction source, GuideUserAction target) throws MaltChainedException {
		source.getAction(actionContainers);
		target.addAction(actionContainers);
	}
	
	public HashMap<String, TableHandler> getTableHandlers() {
		return tableHandlers;
	}

	public String getActionString(GuideUserAction action) throws MaltChainedException {
		final StringBuilder sb = new StringBuilder();
		action.getAction(actionContainers);
		Table ttable = transitionTableHandler.getSymbolTable("TRANS");
		sb.append(ttable.getSymbolCodeToString(transActionContainer.getActionCode()));
		for (int i = 0; i < arcLabelActionContainers.length; i++) {
			if (arcLabelActionContainers[i].getActionCode() != -1) {
				sb.append("+");
				sb.append(arcLabelActionContainers[i].getTable().getSymbolCodeToString(arcLabelActionContainers[i].getActionCode()));
			}
		}
		return sb.toString();
	}
}
