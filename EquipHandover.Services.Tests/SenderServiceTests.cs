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
using EquipHandover.Services.Contracts.Models.Sender;
using FluentAssertions;
using Moq;
using Xunit;

namespace EquipHandover.Services.Tests;

/// <summary>
/// Тесты на <see cref="SenderService"/>
/// </summary>
public class SenderServiceTests : EquipHandoverContextInMemory
{
    private readonly ISenderService senderService;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="SenderServiceTests"/>
    /// </summary>
    public SenderServiceTests()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<ServiceProfile>();
        });
        
        var mapper = config.CreateMapper();
        senderService = new SenderService(mapper,
            UnitOfWork,
            new SenderReadRepository(Context),
            new SenderWriteRepository(Context, Mock.Of<IDateTimeProvider>()));
    }
    
    /// <summary>
    /// GetByIdAsync должен выбросить ошибку о несуществующем Id отправителя с мягким удалением
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldThrowExceptionBySoftDelete()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>(x =>
        {
            x.DeletedAt = DateTimeOffset.UtcNow;
        });
        await Context.AddRangeAsync(sender);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result= () => senderService.GetByIdAsync(sender.Id, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
    
    /// <summary>
    /// GetByIdAsync должен выбросить ошибку о несуществующем Id отправителя
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldThrowExceptionBySenderId()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>();
        await Context.AddRangeAsync(sender);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result= () => senderService.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);
        
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
        var sender = TestEntityProvider.Shared.Create<Sender>();
        await Context.AddRangeAsync(sender);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result = await senderService.GetByIdAsync(sender.Id, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(sender, options => options.ExcludingMissingMembers());
    }
    
    /// <summary>
    /// GetAllAsync должен вернуть значения
    /// </summary>
    [Fact]
    public async Task GetAllAsyncShouldReturnValues()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>();
        var sender1= TestEntityProvider.Shared.Create<Sender>();
        var sender2 = TestEntityProvider.Shared.Create<Sender>(
            x => x.DeletedAt = DateTimeOffset.UtcNow);
        await Context.AddRangeAsync(sender, sender1, sender2);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result = await senderService.GetAllAsync(CancellationToken.None);
        
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
        var result = await senderService.GetAllAsync(CancellationToken.None);
        
        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// CreateAsync должен создать оборудование
    /// </summary>
    [Fact]
    public async Task CreateAsyncShouldCreateSender()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<SenderCreateModel>();

        // Act
        var result = await senderService.CreateAsync(sender, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(sender);
    }
    
    /// <summary>
    /// EditAsync должен отредактировать оборудование
    /// </summary>
    [Fact]
    public async Task EditAsyncShouldEditSender()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>(x =>
        {
            x.FullName = "Хачевский Вадим Викторович";
        });
        await Context.AddRangeAsync(sender);
        await UnitOfWork.SaveChangesAsync();

        var editSender = TestEntityProvider.Shared.Create<SenderCreateModel>(x =>
        {
            x.FullName = "Налакомилсявский Виктор Алексеевич";
        });
        
        // Act
        var result = await senderService.EditAsync(sender.Id, editSender, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(editSender);
    }
    
    /// <summary>
    /// EditAsync должен отредактировать оборудование
    /// </summary>
    [Fact]
    public async Task EditAsyncShouldThrowExceptionBySenderId()
    {
        // Arrange
        var editSender = TestEntityProvider.Shared.Create<SenderCreateModel>(x =>
        {
            x.FullName = "Хачевский Вадим Викторович";
        });
        
        // Act
        var result = () => senderService.EditAsync(Guid.NewGuid(), editSender, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
    
    /// <summary>
    /// DeleteAsync должен удалить оборудование
    /// </summary>
    [Fact]
    public async Task DeleteAsyncShouldDeleteSender()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>();
        await Context.AddRangeAsync(sender);
        await UnitOfWork.SaveChangesAsync();
        // Act
        await senderService.DeleteAsync(sender.Id, CancellationToken.None);
        
        // Assert
        var result = Context.Set<Sender>().Single(x => x.Id == sender.Id);
        result.DeletedAt.Should().NotBeNull();
    }
    
    /// <summary>
    /// DeleteAsync должен выбросить ошибку
    /// </summary>
    [Fact]
    public async Task DeleteAsyncShouldThrowException()
    {
        // Arrange
        var senderId = Guid.NewGuid();
        
        // Act
        var result = () => senderService.DeleteAsync(senderId, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
}