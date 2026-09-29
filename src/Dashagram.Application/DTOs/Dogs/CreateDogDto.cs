namespace Dashagram.Application.Dtos.Dogs
{
    public class CreateDogDto
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Breed { get; set; } // TODO: possible enum for breed?
        public string Bio { get; set; }
    }
}