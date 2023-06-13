using E_CommerceBackEnd.Application.ViewModels.Products;
using FluentValidation;


namespace E_CommerceBackEnd.Application.Validators.Products
{
    public class CreateProductValidator:AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
                RuleFor(p=>p.Name)               
                .NotNull()
                .NotEmpty()
                .WithMessage("Lütfen Ürün adını boş geçmeyiniz.")
                .MaximumLength(150)
                .MinimumLength(5)
                .WithMessage("Lütfen Ürün adını 5 ile 150 Karakter arasında giriniz.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen Stok bilgisini boş geçmeyiniz.")
                .Must(s => s >= 0)
                .WithMessage("Lütfen Stok bilgisini Negatif girmeyiniz.");

            RuleFor(p => p.Price)
               .NotEmpty()
               .NotNull()
               .WithMessage("Lütfen Ürün bilgisini boş geçmeyiniz.")
               .Must(s => s >= 0)
               .WithMessage("Lütfen Ütün bilgisini Negatif girmeyiniz.");



        }

    }
}
