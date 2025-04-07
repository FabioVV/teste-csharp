using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Services.Controllers;

namespace TestAPI
{
    public class TesteApiBancoMovimentarConta
    {
        private readonly IMediator _mediatorMock;

        private readonly ContaController _controller;

        public TesteApiBancoMovimentarConta()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _controller = new ContaController(_mediatorMock);
        }

        [Fact]
        public Task MovimentarConta_ReturnsBadRequest_WhenErrorInvalidTypeOccurs()
        {
            var id = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";

            var command = new MovimentarContaRequest { 
                IdContaCorrente = id,
                Valor = 900.00M,
                TipoMovimento = 'X',
            };

            var errorResponse = new MovimentarContaResponse
            {
                IdMovimento = "0",
                erro = new Errors { Message = "Apenas os tipos “débito” (D) ou “crédito” (C) são aceitos", TipoError = TipoError.INVALID_TYPE.ToString()}
            };

            // Set up the mock to return an error response
            _mediatorMock.Send(Arg.Any<MovimentarContaRequest>())
                .Returns(Task.FromResult(errorResponse));

            // Act
            var result = _controller.MovimentarConta(_mediatorMock, command, id);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().Be(errorResponse.erro);
            return Task.CompletedTask;
        }

        [Fact]
        public Task MovimentarConta_ReturnsBadRequest_WhenErrorInvalidValueOccurs()
        {
            var id = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";

            var command = new MovimentarContaRequest
            {
                IdContaCorrente = id,
                Valor = -900.00M,
                TipoMovimento = 'C',
            };

            var errorResponse = new MovimentarContaResponse
            {
                IdMovimento = "0",
                erro = new Errors { Message = "Apenas valores positivos são aceitos", TipoError = TipoError.INVALID_VALUE.ToString() }
            };

            // Set up the mock to return an error response
            _mediatorMock.Send(Arg.Any<MovimentarContaRequest>())
                .Returns(Task.FromResult(errorResponse));

            // Act
            var result = _controller.MovimentarConta(_mediatorMock, command, id);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().Be(errorResponse.erro);
            return Task.CompletedTask;
        }

        [Fact]
        public Task MovimentarConta_ReturnsBadRequest_WhenErrorInactiveAccountOccurs()
        {
            var id = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";

            var command = new MovimentarContaRequest
            {
                IdContaCorrente = id,
                Valor = 900.00M,
                TipoMovimento = 'C',
            };

            var errorResponse = new MovimentarContaResponse
            {
                IdMovimento = "0",
                erro = new Errors { Message = "Apenas contas correntes ativas podem receber movimentação", TipoError = TipoError.INACTIVE_ACCOUNT.ToString() }
            };

            // Set up the mock to return an error response
            _mediatorMock.Send(Arg.Any<MovimentarContaRequest>())
                .Returns(Task.FromResult(errorResponse));

            // Act
            var result = _controller.MovimentarConta(_mediatorMock, command, id);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().Be(errorResponse.erro);
            return Task.CompletedTask;
        }

        [Fact]
        public Task MovimentarConta_ReturnsBadRequest_WhenErrorInvalidAccountOccurs()
        {
            var id = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";

            var command = new MovimentarContaRequest
            {
                IdContaCorrente = id,
                Valor = 900.00M,
                TipoMovimento = 'C',
            };

            var errorResponse = new MovimentarContaResponse
            {
                IdMovimento = "0",
                erro = new Errors { Message = "Apenas contas correntes cadastradas podem receber movimentação", TipoError = TipoError.INVALID_ACCOUNT.ToString() }
            };

            // Set up the mock to return an error response
            _mediatorMock.Send(Arg.Any<MovimentarContaRequest>())
                .Returns(Task.FromResult(errorResponse));

            // Act
            var result = _controller.MovimentarConta(_mediatorMock, command, id);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().Be(errorResponse.erro);
            return Task.CompletedTask;
        }

        [Fact]
        public Task MovimentarConta_ReturnsSuccess_WhenSuccessful()
        {
            var id = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";

            var command = new MovimentarContaRequest
            {
                IdContaCorrente = id,
                Valor = 900.00M,
                TipoMovimento = 'C',
            };

            var SuccessResponse = new MovimentarContaResponse
            {
                IdMovimento = "1",// Random example id
            };

            // Set up the mock to return an error response
            _mediatorMock.Send(Arg.Any<MovimentarContaRequest>())
                .Returns(Task.FromResult(SuccessResponse));

            // Act
            var result = _controller.MovimentarConta(_mediatorMock, command, id);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().Be(SuccessResponse);
            return Task.CompletedTask;
        }
    }
}