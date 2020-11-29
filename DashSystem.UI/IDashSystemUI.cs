using DashSystem.Models.Transactions;
using DashSystem.Models.Users;

namespace DashSystem.UI
{
    public delegate void CommandEntered(string command);

    public interface IDashSystemUI
    {
        event CommandEntered CommandEnteredEvent;
        void Start();
        void Close();

        #region REGULAR_MESSAGING
        void DisplayGeneralMessage(string message);
        void DisplayUserInfo(IUser user);
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayUserBuysMultipleProducts(uint count, BuyTransaction transaction);
        #endregion

        #region ERROR_MESSAGING
        void DisplayGeneralError(string errorMessage);
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(uint productID);
        void DisplayTooManyArgumentsError(string command);
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        void DisplayIntegersMustBePositive(int argvIndex);

        #endregion

        #region WARNING_MESSAGING
        void DisplayGeneralWarning(string warningMessage);
        void DisplayUserBalanceWarning(IUser user);
        #endregion
    }
}
