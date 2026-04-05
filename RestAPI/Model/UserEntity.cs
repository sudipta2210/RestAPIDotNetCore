namespace RestAPI.Model
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Gender { get; set; }
    }

    public class SignUpEntity
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string EncryptedPwd { get; set; }
    }
}
