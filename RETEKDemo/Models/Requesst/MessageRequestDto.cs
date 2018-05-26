using FluentValidation;

namespace RETEKDemo.Models.Requesst
{
    public class MessageRequestDto
    {
        public string Message { get; set; }
        public int? ParentId { get; set; }
    }
    public class MessageRequestDtoValidation : AbstractValidator<MessageRequestDto>
    {
        public MessageRequestDtoValidation()
        {
            RuleFor(x => x.Message)
                .NotNull()
                .WithMessage("Message must be filled")
                .NotEmpty()
                .WithMessage("Message must be filled");
        }
    }
}
