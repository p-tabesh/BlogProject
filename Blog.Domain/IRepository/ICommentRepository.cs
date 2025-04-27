﻿using Blog.Domain.Entity;
using System.Dynamic;

namespace Blog.Domain.IRepository;

public interface ICommentRepository : ICrudRepository<Comment>
{
    IEnumerable<Comment> GetByArticleId(int articleId);
}
