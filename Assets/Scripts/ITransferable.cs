public interface ITransferable<T> 
{ 
    bool CanTransfer { get;}
    void Add(T item);
    void Remove(T item);
    ITransferable<T> LinkedTransferable { get; set; }
    void EnableTransfer(ITransferable<T> transferable);
    void CloseTransfer();
}
