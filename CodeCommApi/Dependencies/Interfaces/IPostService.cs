using CodeCommApi.Models;

namespace CodeCommApi.Dependencies.Interfaces
{
    public interface IPostService
    {
        Task<Post> CreatePost(Post Post);
        Task<List<Post>> GetAllPosts();
        Task<List<Post>> GetAvailablePosts();
        Task<List<Post>> GetUserPosts(Guid UserId);
        Task<Post> EditPost(Guid PostId, Post Post);
        Task<bool> DeletePost(Guid PostId);
        Task<Post> GetPostById(Guid PostId);
    }
}
