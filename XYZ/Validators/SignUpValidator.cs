using FastEndpoints;
using FluentValidation;
using XYZ.Endpoints.Requests;

namespace XYZ.Validators
{
    public class SignUpValidator : Validator<SignUpRequest>
    {
        public SignUpValidator() {
            RuleFor(x => x.username)
                .NotEmpty()
                .WithMessage("Username is required")
                .MinimumLength(5)
                .WithMessage("Your name is too short");
            RuleFor(x => x.email)
                .NotEmpty()
                .WithMessage("Email can not be empty")
                .EmailAddress()
                .WithMessage("Wrong email format");
            RuleFor(x => x.password.Equals(x.passwordConfirm))
                .NotEmpty()
                .WithMessage("Passwords are not the same");
        }
    }
}
