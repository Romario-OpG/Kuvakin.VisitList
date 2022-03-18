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
            RuleFor(x => x.LastNameTB.Text).NotNull().NotEmpty().WithMessage("Введите фамилию\n");

            RuleFor(x => x.FirstNameTB.Text).NotNull().NotEmpty().WithMessage("Введите имя\n");

            RuleFor(x => x.MiddleNameTB.Text).NotNull().NotEmpty().WithMessage("Введите отчество\n");
        }
    }
}
