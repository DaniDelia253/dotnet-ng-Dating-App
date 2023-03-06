using API.Entities;

namespace API.Interfaces
//this interface is a contract that says that any other class I create that inplements this interface has to support this method with this return value adn these specific arguments
{
    public interface ITokenService
    {
        //supports a single method called createToken that returns a string and takes an AppUser called user
        string CreateToken(AppUser user);
    }
}