using FluentValidation;
using MovieDatabase.Application.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.DTOs.Validation;

public class CreatePersonValidator : AbstractValidator<CreatePersonRequest>
{

    public CreatePersonValidator()
    {
       

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Jméno je povinné");


    }
}
