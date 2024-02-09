using AutoMapper;
using CodeCommApi.Data;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dto.Users.Response;
using CodeCommApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Dependencies.Services
{
    public class PostService : IPostService
    {
        private readonly CodeCommDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _user;

        public PostService(IMapper mapper, CodeCommDbContext context, IUserService user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }

        public async Task<Post> CreatePost(Post Post)
        {
            try
            {
                var user = await _user.FindUser(Post.UserId);
                if (user == null)
                {
                    throw new Exception("UNABLE TO GET USER || INVALID USER ID");
                }
                Post.User = user;
                Post.PostId=Guid.NewGuid();
                var post = await _context.Posts.AddAsync(Post);
                await _context.SaveChangesAsync();
                return Post;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeletePost(Guid PostId)
        {
            try
            {
                var post = await _context.Posts.FindAsync(PostId);
                if (post == null)
                {
                    throw new Exception("POST NOT FOUND || INVALID POST ID");
                }
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Post> EditPost(Guid PostId, Post Post)
        {
            try
            {
                var post = await _context.Posts.FindAsync(PostId);
                if (post == null)
                {
                    throw new Exception("UNABLE TO FIND POST || INVALID POST ID");
                }
                _mapper.Map(Post, post);
                await _context.SaveChangesAsync();
                return post;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<Post>> GetAllPosts()
        {
            try
            {
                return await _context.Posts.Include(x=>x.User).ToListAsync();
                
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<Post>> GetAvailablePosts()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetUserPosts(Guid UserId)
        {
            try
            {
                return await _context.Posts.Where(x => x.UserId == UserId).ToListAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public async Task<Post> GetPostById(Guid PostId)
        {
            try
            {
                return await _context.Posts.FirstOrDefaultAsync(x => x.UserId == PostId);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
