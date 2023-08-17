public interface ITransferable<T> 
{ 
    bool CanTransfer { get;}
    void Add(T item);
    void Remove(T item);
}
