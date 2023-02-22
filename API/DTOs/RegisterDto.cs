using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        //at this stage of the course..
        //inside this DTO is a good place to do validation
        //BC inside the API controller, I have that [ApiController] attribute that has a couple of powers
        //one of the powers is that it automatically binds to the parameters inside the method (this is the DTO in the case of the account controller Register method)
        //before this was available I would have had to tell the framework where to look for the thing (dto) that I was passing intot he controller
        //^^I would have done this by adding a [FromBody] attribute to tell it that the dto comes fromt he body of hte request
        //BUT bc i have that [ApiController] attribute in my base api controller class, I don't have to do that anymore
        //ANOTHER thing that api controller attribute gives me is that it is automatically going to check validation before we even get to the controller....
        //SO I do the validation here :)

        [Required]
        //this attribute comes from the DataAnnotations namespace and there are lots I can use (phone number, max length, string, create-your-own-regex)
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}