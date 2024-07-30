using System.Collections.Generic;
using System.ServiceModel;

[ServiceContract]
public interface IYourService
{
    // Lookup operations
    [OperationContract]
    List<Lookup> GetLookups();

    [OperationContract]
    Lookup GetLookupById(int id);

    [OperationContract]
    void AddLookup(Lookup lookup);

    [OperationContract]
    void UpdateLookup(Lookup lookup);

    [OperationContract]
    void DeleteLookup(int id);

    // Account operations
    [OperationContract]
    List<Account> GetAccounts();

    [OperationContract]
    Account GetAccountById(int id);

    [OperationContract]
    void AddAccount(Account account);

    [OperationContract]
    void UpdateAccount(Account account);

    [OperationContract]
    void DeleteAccount(int id);

    // Transaction operations
    [OperationContract]
    List<Transaction> GetTransactions();

    [OperationContract]
    Transaction GetTransactionById(int id);

    [OperationContract]
    void AddTransaction(Transaction transaction);

    [OperationContract]
    void UpdateTransaction(Transaction transaction);

    [OperationContract]
    void DeleteTransaction(int id);

    // Repeat for Address and AccountHolder
}
