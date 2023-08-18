using System;

//It implies an entity that can transfer and be transfered to.
public interface ITransferable<T> 
{ 
    bool CanTransfer { get;}
    void Add(T item);
    void Remove(T item);

    //Other ITransferable which it can transfer to
    ITransferable<T> LinkedTransferable { get; set; }

    //Links an ITransferable to transfer to and also it's requirement
    void EnableTransfer(ITransferable<T> transferable, Func<T, bool> transferRequirement);
    void CloseTransfer();
}
