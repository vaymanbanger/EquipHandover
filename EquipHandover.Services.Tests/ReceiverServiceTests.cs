using Ahatornn.TestGenerator;
using AutoMapper;
using EquipHandover.Common.Contracts;
using EquipHandover.Context.Tests;
using EquipHandover.Entities;
using EquipHandover.Repositories.ReadRepositories;
using EquipHandover.Repositories.WriteRepositories;
using EquipHandover.Services.AutoMappers;
using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Exceptions;
using EquipHandover.Services.Contracts.Models.Receiver;
using FluentAssertions;
using Moq;
using Xunit;

namespace EquipHandover.Services.Tests;

/// <summary>
/// Тесты на <see cref="ReceiverService"/>
/// </summary>
public class ReceiverServiceTests : EquipHandoverContextInMemory
{
    private readonly IReceiverService receiverService;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ReceiverServiceTests"/>
    /// </summary>
    public ReceiverServiceTests()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<ServiceProfile>();
        });
        
        var mapper = config.CreateMapper();
        receiverService = new ReceiverService(mapper,
            UnitOfWork,
            new ReceiverReadRepository(Context),
            new ReceiverWriteRepository(Context, Mock.Of<IDateTimeProvider>()));
    }
    
    /// <summary>
    /// GetByIdAsync должен выбросить ошибку о несуществующем Id принимающего с мягким удалением
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldThrowExceptionBySoftDelete()
    {
        // Arrange
        var receiver = TestEntityProvider.Shared.Create<Receiver>(x =>
        {
            x.DeletedAt = DateTimeOffset.UtcNow;
        });
        await Context.AddRangeAsync(receiver);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result= () => receiverService.GetByIdAsync(receiver.Id, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
    
    /// <summary>
    /// GetByIdAsync должен выбросить ошибку о несуществующем Id принимающего
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldThrowExceptionByReceiverId()
    {
        // Arrange
        var receiver = TestEntityProvider.Shared.Create<Receiver>();
        await Context.AddRangeAsync(receiver);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result= () => receiverService.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }

    /// <summary>
    /// GetByIdAsync должен вернуть значение
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldReturnValue()
    {
        // Arrange
        var receiver = TestEntityProvider.Shared.Create<Receiver>();
        await Context.AddRangeAsync(receiver);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result = await receiverService.GetByIdAsync(receiver.Id, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(receiver, options => options.ExcludingMissingMembers());
    }
    
    /// <summary>
    /// GetAllAsync должен вернуть значения
    /// </summary>
    [Fact]
    public async Task GetAllAsyncShouldReturnValues()
    {
        // Arrange
        var receiver = TestEntityProvider.Shared.Create<Receiver>();
        var receiver1= TestEntityProvider.Shared.Create<Receiver>();
        var receiver2 = TestEntityProvider.Shared.Create<Receiver>(
            x => x.DeletedAt = DateTimeOffset.UtcNow);
        await Context.AddRangeAsync(receiver, receiver1, receiver2);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result = await receiverService.GetAllAsync(CancellationToken.None);
        
        // Assert
        result.Should().HaveCount(2);
    }
    
    /// <summary>
    /// GetAllAsync должен быть пустым
    /// </summary>
    [Fact]
    public async Task GetAllAsyncShouldBeEmpty()
    {
        // Act
        var result = await receiverService.GetAllAsync(CancellationToken.None);
        
        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// CreateAsync должен создать принимающего
    /// </summary>
    [Fact]
    public async Task CreateAsyncShouldCreateReceiver()
    {
        // Arrange
        var receiver = TestEntityProvider.Shared.Create<ReceiverCreateModel>();

        // Act
        var result = await receiverService.CreateAsync(receiver, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(receiver);
    }
    
    /// <summary>
    /// EditAsync должен отредактировать принимающего
    /// </summary>
    [Fact]
    public async Task EditAsyncShouldEditReceiver()
    {
        // Arrange
        var receiver = TestEntityProvider.Shared.Create<Receiver>(x =>
        {
            x.FullName = "Хачевский Вадим Викторович";
        });
        await Context.AddRangeAsync(receiver);
        await UnitOfWork.SaveChangesAsync();

        var editReceiver = TestEntityProvider.Shared.Create<ReceiverCreateModel>(x =>
        {
            x.FullName = "Налакомилсявский Виктор Алексеевич";
        });
        
        // Act
        var result = await receiverService.EditAsync(receiver.Id, editReceiver, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(editReceiver);
    }
    
    /// <summary>
    /// EditAsync должен бросить ошибку о несуществующем Id принимающего
    /// </summary>
    [Fact]
    public async Task EditAsyncShouldThrowExceptionByReceiverId()
    {
        // Arrange
        var editReceiver = TestEntityProvider.Shared.Create<ReceiverCreateModel>(x =>
        {
            x.FullName = "Хачевский Вадим Викторович";
        });
        
        // Act
        var result = () => receiverService.EditAsync(Guid.NewGuid(), editReceiver, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
    
    /// <summary>
    /// DeleteAsync должен удалить принимающего
    /// </summary>
    [Fact]
    public async Task DeleteAsyncShouldDeleteReceiver()
    {
        // Arrange
        var receiver = TestEntityProvider.Shared.Create<Receiver>();
        await Context.AddRangeAsync(receiver);
        await UnitOfWork.SaveChangesAsync();
        // Act
        await receiverService.DeleteAsync(receiver.Id, CancellationToken.None);
        
        // Assert
        var result = Context.Set<Receiver>().Single(x => x.Id == receiver.Id);
        result.DeletedAt.Should().NotBeNull();
    }
    
    /// <summary>
    /// DeleteAsync должен выбросить ошибку
    /// </summary>
    [Fact]
    public async Task DeleteAsyncShouldThrowException()
    {
        // Arrange
        var receiverId = Guid.NewGuid();
        
        // Act
        var result = () => receiverService.DeleteAsync(receiverId, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
}