using GlaucomaWay.Models;
using GlaucomaWay.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using GlaucomaWay.Controllers;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using GlaucomaWay.Users;
using Microsoft.AspNetCore.Http;

namespace Tests.UnitTests;

public class Vf14ControllerTests
{
    private readonly Mock<IVf14Repository> _Vf14repositoryStub = new();
    private readonly Mock<IPatientRepository> _patientRepositoryStub = new();
    private readonly Mock<ILogger<Vf14Controller>> _loggerStub = new();
    private readonly Mock<UserManager<User>> _userManagerStub = new();
    private readonly Mock<HttpContextAccessor> _HttpContextAccessorStub = new();

    private readonly Random _rand = new();

    [Fact]
    public async Task GetByIdAsync_WithNonExistingItem_ReturnsNotFound()
    {
        _Vf14repositoryStub.Setup(repo => repo.GetByIdWithPatientAsync(It.IsAny<int>(), new CancellationToken()))
            .ReturnsAsync((Vf14ResultModel) null);

        var controller = new Vf14Controller(_Vf14repositoryStub.Object, _patientRepositoryStub.Object,
            _userManagerStub.Object, _HttpContextAccessorStub.Object, _loggerStub.Object);

        var result = await controller.GetByIdAsync(_rand.Next(), new CancellationToken());

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingItem_ReturnsExpectedItem()
    {
        var expectedItem = CreateRandomVf14ResultModel();

        _Vf14repositoryStub.Setup(repo => repo.GetByIdWithPatientAsync(It.IsAny<int>(), new CancellationToken()))
            .ReturnsAsync(expectedItem);

        var controller = new Vf14Controller(_Vf14repositoryStub.Object, _patientRepositoryStub.Object,
            _userManagerStub.Object, _HttpContextAccessorStub.Object, _loggerStub.Object);

        var result = await controller.GetByIdAsync(_rand.Next(), new CancellationToken());

        var resultData = (OkObjectResult) result.Result;

        resultData.Value.Should().BeEquivalentTo(expectedItem);
    }

    [Fact]
    public async Task GetAllAsync_WithUnexistingItem_ReturnsNotFound()
    {
        _Vf14repositoryStub.Setup(repo => repo.GetAllAsync(new CancellationToken()))
            .ReturnsAsync((List<Vf14ResultModel>) null);

        var controller = new Vf14Controller(_Vf14repositoryStub.Object, _patientRepositoryStub.Object,
            _userManagerStub.Object, _HttpContextAccessorStub.Object, _loggerStub.Object);

        var result = await controller.GetAllAsync(new CancellationToken());

        result.Result.Should().BeOfType<OkObjectResult>();

        var resultData = (OkObjectResult) result.Result;

        resultData.Value.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingItems_ReturnsExpectedItems()
    {
        var expectedItems = new List<Vf14ResultModel> {CreateRandomVf14ResultModel(), CreateRandomVf14ResultModel()};

        _Vf14repositoryStub.Setup(repo => repo.GetAllAsync(new CancellationToken()))
            .ReturnsAsync(expectedItems);

        var controller = new Vf14Controller(_Vf14repositoryStub.Object, _patientRepositoryStub.Object,
            _userManagerStub.Object, _HttpContextAccessorStub.Object, _loggerStub.Object);

        var result = await controller.GetAllAsync(new CancellationToken());

        var resultData = (OkObjectResult) result.Result;

        resultData.Value.Should().BeEquivalentTo(expectedItems);
    }


    [Fact]
    public async Task CreateAsync_WithNotExistingPatient_ReturnsBadRequest()
    {
        _patientRepositoryStub.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), new CancellationToken()))
            .ReturnsAsync((PatientModel) null);

        var controller = new Vf14Controller(_Vf14repositoryStub.Object, _patientRepositoryStub.Object,
            _userManagerStub.Object, _HttpContextAccessorStub.Object, _loggerStub.Object);

        var result =
            await controller.CreateAsync(CreateRandomVf14CreateOrUpdateModel(_rand.Next()), new CancellationToken());

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task CreateAsync_WithExistingPatient_CreatesAndReturnsId()
    {
        var expectedPatient = CreateRandomPatientModel();
        var expectedVf14Result = CreateRandomVf14ResultModel();

        _patientRepositoryStub.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), new CancellationToken()))
            .ReturnsAsync(expectedPatient);

        _Vf14repositoryStub.Setup(repo => repo.CreateAsync(It.IsAny<Vf14ResultModel>(), new CancellationToken()))
            .ReturnsAsync(expectedVf14Result);

        var controller = new Vf14Controller(_Vf14repositoryStub.Object, _patientRepositoryStub.Object,
            _userManagerStub.Object, _HttpContextAccessorStub.Object, _loggerStub.Object);

        var result = await controller.CreateAsync(CreateRandomVf14CreateOrUpdateModel(expectedPatient.Id),
            new CancellationToken());

        result.Result.Should().BeOfType<CreatedAtActionResult>();

        var resultId = (int) ((CreatedAtActionResult) result.Result).Value;

        resultId.Should().Be(expectedVf14Result.Id);
    }


    private Vf14ResultModel CreateRandomVf14ResultModel() =>
        new()
        {
            Id = _rand.Next(),
            Date = Faker.Identification.DateOfBirth(),
            Patient = CreateRandomPatientModel()
        };

    private Vf14CreateOrUpdateModel CreateRandomVf14CreateOrUpdateModel(int patientId) =>
        new()
        {
            Date = Faker.Identification.DateOfBirth(),
        };

    private PatientModel CreateRandomPatientModel() =>
        new()
        {
            Id = _rand.Next(),
            BithDate = Faker.Identification.DateOfBirth(),
        };
}