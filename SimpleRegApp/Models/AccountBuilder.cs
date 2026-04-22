namespace SimpleRegApp.Models
{
    public class AccountBuilder
    {
        private Account _account = new Account();

        public AccountBuilder SetFirstName(string firstName)
        {
            _account.FirstName = firstName;
            return this;
        }

        public AccountBuilder SetLastName(string lastName)
        {
            _account.LastName = lastName;
            return this;
        }
        public AccountBuilder SetEmail(string email)
        {
            _account.Email = email;
            return this;
        }
        public AccountBuilder SetUsername(string username)
        {
            _account.Username = username;
            return this;
        }
        public AccountBuilder SetPassword(string password)
        {
            _account.Password = password;
            return this;
        }

        public Account Build()
        {
            return _account;
        }
    }
}
