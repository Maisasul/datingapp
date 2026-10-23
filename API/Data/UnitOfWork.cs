using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private IMemberRepository? _memberRepository;
        private IMessageRepository? _messageRepository;
        private ILikesRepository? _likeRepository;
        public IMemberRepository MemberRepository => _memberRepository ??= new MemberRepository(context);

        public IMessageRepository MessageRepository => _messageRepository ??= new MessageRepository(context);

        public ILikesRepository LikesRepository => _likeRepository ??= new LikesRepository(context);

        public async Task<bool> Complete()
        {
            try
            {
                return await context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("?an error occuerd while saving changes", ex);
            }
        }

        public bool HasChanges()
        {
            return context.ChangeTracker.HasChanges();
        }
    }
}