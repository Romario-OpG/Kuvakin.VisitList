using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.Forms
{
    internal class TextBoxValidator : AbstractValidator<TextBoxModel>
    {
        public TextBoxValidator()
        {
            RuleFor(x => x.LastName.Text).NotNull().NotEmpty().WithMessage("Введите фамилию");

            RuleFor(x => x.FirstName.Text).NotNull().NotEmpty().WithMessage("Введите имя");

            RuleFor(x => x.MiddleName.Text).NotNull().NotEmpty().WithMessage("Введите отчество");
        }
    }
}
