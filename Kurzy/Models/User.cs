namespace Kurzy.Models
{
    public class User
    {
        public int Id { get; set; }

        private string _firstName = null!;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                UpdateUserName();
            }
        }

        private string _lastName = null!;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                UpdateUserName();
            }
        }

        private string _userName = null!;
        public string UserName
        {
            get => _userName;
            private set => _userName = value;
        }

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth.Date;
            set => _dateOfBirth = value.Date;
        }

        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; }
        public List<CourseStudent> CoursesStudents { get; set; } = new List<CourseStudent>();

        public User()
        {
            UpdateUserName();
        }

        private void UpdateUserName()
        {
            _userName = $"{FirstName} {LastName}";
        }
    }
}
