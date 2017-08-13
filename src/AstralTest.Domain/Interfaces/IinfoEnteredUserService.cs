using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для добавления информации о том, к чему обращался пользователь
    /// </summary>                                                              
    public interface IInfoEnteredUserService
    {
      /// <summary>
      /// Содержит информацию о том, к чему обращались пользователи
      /// </summary>
      List<InfoAboutEnteredUser> InfoUsers { get; }

        Task<Guid> AddAsync();


    }
}
