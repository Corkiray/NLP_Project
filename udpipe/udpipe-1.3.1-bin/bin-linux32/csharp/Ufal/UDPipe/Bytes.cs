//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.1.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Ufal.UDPipe {

public class Bytes : global::System.IDisposable, global::System.Collections.IEnumerable, global::System.Collections.Generic.IList<byte>
 {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Bytes(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Bytes obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(Bytes obj) {
    if (obj != null) {
      if (!obj.swigCMemOwn)
        throw new global::System.ApplicationException("Cannot release ownership as memory is not owned");
      global::System.Runtime.InteropServices.HandleRef ptr = obj.swigCPtr;
      obj.swigCMemOwn = false;
      obj.Dispose();
      return ptr;
    } else {
      return new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
    }
  }

  ~Bytes() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          udpipe_csharpPINVOKE.delete_Bytes(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public Bytes(global::System.Collections.IEnumerable c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (byte element in c) {
      this.Add(element);
    }
  }

  public Bytes(global::System.Collections.Generic.IEnumerable<byte> c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (byte element in c) {
      this.Add(element);
    }
  }

  public bool IsFixedSize {
    get {
      return false;
    }
  }

  public bool IsReadOnly {
    get {
      return false;
    }
  }

  public byte this[int index]  {
    get {
      return getitem(index);
    }
    set {
      setitem(index, value);
    }
  }

  public int Capacity {
    get {
      return (int)capacity();
    }
    set {
      if (value < 0 || (uint)value < size())
        throw new global::System.ArgumentOutOfRangeException("Capacity");
      reserve((uint)value);
    }
  }

  public int Count {
    get {
      return (int)size();
    }
  }

  public bool IsSynchronized {
    get {
      return false;
    }
  }

  public void CopyTo(byte[] array)
  {
    CopyTo(0, array, 0, this.Count);
  }

  public void CopyTo(byte[] array, int arrayIndex)
  {
    CopyTo(0, array, arrayIndex, this.Count);
  }

  public void CopyTo(int index, byte[] array, int arrayIndex, int count)
  {
    if (array == null)
      throw new global::System.ArgumentNullException("array");
    if (index < 0)
      throw new global::System.ArgumentOutOfRangeException("index", "Value is less than zero");
    if (arrayIndex < 0)
      throw new global::System.ArgumentOutOfRangeException("arrayIndex", "Value is less than zero");
    if (count < 0)
      throw new global::System.ArgumentOutOfRangeException("count", "Value is less than zero");
    if (array.Rank > 1)
      throw new global::System.ArgumentException("Multi dimensional array.", "array");
    if (index+count > this.Count || arrayIndex+count > array.Length)
      throw new global::System.ArgumentException("Number of elements to copy is too large.");
    for (int i=0; i<count; i++)
      array.SetValue(getitemcopy(index+i), arrayIndex+i);
  }

  public byte[] ToArray() {
    byte[] array = new byte[this.Count];
    this.CopyTo(array);
    return array;
  }

  global::System.Collections.Generic.IEnumerator<byte> global::System.Collections.Generic.IEnumerable<byte>.GetEnumerator() {
    return new BytesEnumerator(this);
  }

  global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() {
    return new BytesEnumerator(this);
  }

  public BytesEnumerator GetEnumerator() {
    return new BytesEnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class BytesEnumerator : global::System.Collections.IEnumerator
    , global::System.Collections.Generic.IEnumerator<byte>
  {
    private Bytes collectionRef;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public BytesEnumerator(Bytes collection) {
      collectionRef = collection;
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public byte Current {
      get {
        if (currentIndex == -1)
          throw new global::System.InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new global::System.InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new global::System.InvalidOperationException("Collection modified.");
        return (byte)currentObject;
      }
    }

    // Type-unsafe IEnumerator.Current
    object global::System.Collections.IEnumerator.Current {
      get {
        return Current;
      }
    }

    public bool MoveNext() {
      int size = collectionRef.Count;
      bool moveOkay = (currentIndex+1 < size) && (size == currentSize);
      if (moveOkay) {
        currentIndex++;
        currentObject = collectionRef[currentIndex];
      } else {
        currentObject = null;
      }
      return moveOkay;
    }

    public void Reset() {
      currentIndex = -1;
      currentObject = null;
      if (collectionRef.Count != currentSize) {
        throw new global::System.InvalidOperationException("Collection modified.");
      }
    }

    public void Dispose() {
        currentIndex = -1;
        currentObject = null;
    }
  }

  public void Clear() {
    udpipe_csharpPINVOKE.Bytes_Clear(swigCPtr);
  }

  public void Add(byte x) {
    udpipe_csharpPINVOKE.Bytes_Add(swigCPtr, x);
  }

  private uint size() {
    uint ret = udpipe_csharpPINVOKE.Bytes_size(swigCPtr);
    return ret;
  }

  private uint capacity() {
    uint ret = udpipe_csharpPINVOKE.Bytes_capacity(swigCPtr);
    return ret;
  }

  private void reserve(uint n) {
    udpipe_csharpPINVOKE.Bytes_reserve(swigCPtr, n);
  }

  public Bytes() : this(udpipe_csharpPINVOKE.new_Bytes__SWIG_0(), true) {
  }

  public Bytes(Bytes other) : this(udpipe_csharpPINVOKE.new_Bytes__SWIG_1(Bytes.getCPtr(other)), true) {
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public Bytes(int capacity) : this(udpipe_csharpPINVOKE.new_Bytes__SWIG_2(capacity), true) {
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  private byte getitemcopy(int index) {
    byte ret = udpipe_csharpPINVOKE.Bytes_getitemcopy(swigCPtr, index);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private byte getitem(int index) {
    byte ret = udpipe_csharpPINVOKE.Bytes_getitem(swigCPtr, index);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void setitem(int index, byte val) {
    udpipe_csharpPINVOKE.Bytes_setitem(swigCPtr, index, val);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddRange(Bytes values) {
    udpipe_csharpPINVOKE.Bytes_AddRange(swigCPtr, Bytes.getCPtr(values));
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public Bytes GetRange(int index, int count) {
    global::System.IntPtr cPtr = udpipe_csharpPINVOKE.Bytes_GetRange(swigCPtr, index, count);
    Bytes ret = (cPtr == global::System.IntPtr.Zero) ? null : new Bytes(cPtr, true);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Insert(int index, byte x) {
    udpipe_csharpPINVOKE.Bytes_Insert(swigCPtr, index, x);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void InsertRange(int index, Bytes values) {
    udpipe_csharpPINVOKE.Bytes_InsertRange(swigCPtr, index, Bytes.getCPtr(values));
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAt(int index) {
    udpipe_csharpPINVOKE.Bytes_RemoveAt(swigCPtr, index);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveRange(int index, int count) {
    udpipe_csharpPINVOKE.Bytes_RemoveRange(swigCPtr, index, count);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public static Bytes Repeat(byte value, int count) {
    global::System.IntPtr cPtr = udpipe_csharpPINVOKE.Bytes_Repeat(value, count);
    Bytes ret = (cPtr == global::System.IntPtr.Zero) ? null : new Bytes(cPtr, true);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Reverse() {
    udpipe_csharpPINVOKE.Bytes_Reverse__SWIG_0(swigCPtr);
  }

  public void Reverse(int index, int count) {
    udpipe_csharpPINVOKE.Bytes_Reverse__SWIG_1(swigCPtr, index, count);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRange(int index, Bytes values) {
    udpipe_csharpPINVOKE.Bytes_SetRange(swigCPtr, index, Bytes.getCPtr(values));
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool Contains(byte value) {
    bool ret = udpipe_csharpPINVOKE.Bytes_Contains(swigCPtr, value);
    return ret;
  }

  public int IndexOf(byte value) {
    int ret = udpipe_csharpPINVOKE.Bytes_IndexOf(swigCPtr, value);
    return ret;
  }

  public int LastIndexOf(byte value) {
    int ret = udpipe_csharpPINVOKE.Bytes_LastIndexOf(swigCPtr, value);
    return ret;
  }

  public bool Remove(byte value) {
    bool ret = udpipe_csharpPINVOKE.Bytes_Remove(swigCPtr, value);
    return ret;
  }

}

}