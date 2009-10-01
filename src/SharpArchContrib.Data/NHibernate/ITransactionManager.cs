using PostSharp.Laos;

namespace SharpArchContrib.Data.NHibernate
{
    public interface ITransactionManager
    {
        int TransactionDepth { get; }
        void PushTransaction(string factoryKey, MethodExecutionEventArgs eventArgs);
        bool TransactionIsActive(string factoryKey);
        void PopTransaction(string factoryKey, MethodExecutionEventArgs eventArgs);
        void RollbackTransaction(string factoryKey, MethodExecutionEventArgs eventArgs);
        void CommitTransaction(string factoryKey, MethodExecutionEventArgs eventArgs);
    }
}