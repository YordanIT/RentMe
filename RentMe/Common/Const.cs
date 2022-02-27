namespace RentMe.Common
{
    public static class Const
    {
        //User
        public const int UsernameMinLenght = 3;
        public const int UsernameMaxLenght = 50;
        public const int PasswordMinLenght = 6;
        public const int PasswordMaxLenght = 20;
        public const int HashedPasswordMaxLength = 64;
        public const int FirstNameMaxLength = 100;
        public const int LastNameMaxLength = 100;
        public const int EmailMaxLength = 100;
        public const int PhoneMaxLength = 15;
        public const int IbanMaxLength = 34;
        public const string Tenant = nameof(Tenant);
        public const string Landlord = nameof(Landlord);

        //Expense

        //Property
        public const int PropertyTypeMinLength = 3;
        public const int PropertyTypeMaxLength = 100;

        //Advertisement
        public const int ImageMaxValue = 1048576;
    }
}
