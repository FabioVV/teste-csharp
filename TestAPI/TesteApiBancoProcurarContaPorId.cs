using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Services.Controllers;

namespace TestAPI
{
    public class TesteApiBancoProcurarContaPorId
    {
        private readonly IMediator _mediatorMock;

        private readonly ContaController _controller;

        public TesteApiBancoProcurarContaPorId()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _controller = new ContaController(_mediatorMock);
        }

        [Fact]
        public Task ProcurarContaPorId_ReturnsBadRequest_WhenErrorInvalidAccountOccurs()
        {
            var id = "123";

            var command = new ProcurarContaPorIdRequest(id);

            var errorResponse = new ProcurarContaPorIdResponse
            {
                erro = new Errors { Message = "Apenas contas correntes cadastradas podem receber movimentação", TipoError = TipoError.INVALID_ACCOUNT.ToString() }
            };

            _mediatorMock.Send(Arg.Any<ProcurarContaPorIdRequest>())
                .Returns(Task.FromResult(errorResponse));

            var result = _controller.ConsultarConta(_mediatorMock, id);

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().Be(errorResponse.erro);
            return Task.CompletedTask;
        }

        [Fact]
        public Task ProcurarContaPorId_ReturnsBadRequest_WhenErrorInactiveAccountOccurs()
        {
            var id = "D2E02051-7067-ED11-94C0-835DFA4A16C9";

            var command = new ProcurarContaPorIdRequest(id);

            var errorResponse = new ProcurarContaPorIdResponse
            {
                erro = new Errors { Message = "Apenas contas correntes ativas podem receber movimentação", TipoError = TipoError.INACTIVE_ACCOUNT.ToString() }
            };

            _mediatorMock.Send(Arg.Any<ProcurarContaPorIdRequest>())
                .Returns(Task.FromResult(errorResponse));

            var result = _controller.ConsultarConta(_mediatorMock, id);

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().Be(errorResponse.erro);
            return Task.CompletedTask;
        }

        [Fact]
        public Task ProcurarContaPorId_ReturnsSuccess_WhenSuccessful()
        {
            var id = "D2E02051-7067-ED11-94C0-835DFA4A16C9";

            var command = new ProcurarContaPorIdRequest(id);

            var SuccessResponse = new ProcurarContaPorIdResponse
            {
                Idcontacorrente = id, 
                Titular = "Katherine Sanchez",
                Saldo = 0, 
                HoralConsulta = DateTime.Now,
                erro = null,
            };

            _mediatorMock.Send(Arg.Any<ProcurarContaPorIdRequest>())
                .Returns(Task.FromResult(SuccessResponse));

            var result = _controller.ConsultarConta(_mediatorMock, id);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().Be(SuccessResponse);
            return Task.CompletedTask;
        }
    }
}