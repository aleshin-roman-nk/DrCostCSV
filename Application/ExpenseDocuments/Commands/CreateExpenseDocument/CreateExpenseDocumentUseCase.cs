using Application.Abstractions;
using Application.Abstractions.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.Commands.CreateExpenseDocument;

public class CreateExpenseDocumentUseCase
{
	private readonly IExpenseDocumentRepository _repository;
	private readonly IUnitOfWork _uow;

	public CreateExpenseDocumentUseCase(
	IExpenseDocumentRepository repository,
	IUnitOfWork uow)
	{
		_repository = repository;
		_uow = uow;
	}

	public void ExecuteAsync(CreateExpenseDocumentCommand cmd)
	{
		if (string.IsNullOrWhiteSpace(cmd.SellerName))
			throw new ArgumentException("Seller name is required.");

		var document = new ExpenseDocument(0, cmd.SellerName, cmd.Date);

		foreach (var itemDto in cmd.Items)
		{
			if (string.IsNullOrWhiteSpace(itemDto.Name))
				throw new ArgumentException("Item name is required.");

			if (itemDto.Price is null)
				throw new ArgumentException("Price is required.");

			if (itemDto.Amount is null)
				throw new ArgumentException("Amount is required.");

			var item = new ExpenseDocumentItem(
				0,
				itemDto.Name,
				itemDto.Price.Value,
				itemDto.Amount.Value,
				itemDto.CategoryId);

			document.Items.Add(item);
		}

		_repository.Add(document);
		_uow.SaveChanges();
	}
}
