using System.Threading.Tasks;
using Application.Core.Utilities.Result;
using DefaultNamespace;

namespace Application.Business.Abstract;

public interface IAOPExampleManager
{
    /// <summary>
    /// Sunucu hatalarını temsil eden örnek method.
    /// </summary>
    /// <returns></returns>
    IDataResult<string> ErrorTest();
    /// <summary>
    /// Performans hatalarını temsil eden örnek method.
    /// </summary>
    /// <returns></returns>
    Task<IDataResult<string>> PerformanceTest();

    /// <summary>
    /// Validasyon işlemini AOP ile örneğini gösteren örnek method.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    IDataResult<BaseIdDTO> ValidationTest(BaseIdDTO request);
    
    /// <summary>
    /// Sunucu taraflı önbellekleme işlemini AOP entegrasyonu ile örneğini gösteren method.
    /// Performans aspecti bu methodun performansını ölçecektir. Eğer performans sınırlarının
    /// üzerinde çalışırsa, performans log gönderecektir. Herhangi bir hata oluşursa exception
    /// aspect devreye girecektir ve hata log gönderecektir. Cache aspect ise methodu sunucu
    /// taraflı önbellekleme yapar.
    /// </summary>
    /// <returns></returns>
    IDataResult<string> CacheTest();
    
    /// <summary>
    /// Patterni verilen cache verilerini temizleyen örnek method.
    /// </summary>
    /// <returns></returns>
    IResult ClearCacheTest();
}