namespace RentMe.Core.Common
{
    public class Const
    {
        //User
        public const string RoleAdmin = "Admin";
        public const int FirstNameMinLength = 2;
        public const int FirstNameMaxLength = 100;
        public const int LastNameMinLength = 2;
        public const int LastNameMaxLength = 100;
  
        //Expense
        public const int CommentMaxLength = 200;

        //Property
        public const int PropertyTypeMinLength = 3;
        public const int PropertyTypeMaxLength = 100;
        public const int CityMinLength = 2;
        public const int CityMaxLength = 100;
        public const int AddressMinLength = 10;
        public const int AddressMaxLength = 200;
        public const double AreaMinLength = 5;
        public const double AreaMaxLength = 2000;

        //Advertisement
        public const int ImageMaxValue = 1048576;
        public const int DescriptionMaxLength = 200;

        //Article
        public const int TitleMinLength = 5;
        public const int TitleMaxLength = 50;
        public const int ContentMinLength = 40;
        public const int ContentMaxLength = 4000;
    }
}
