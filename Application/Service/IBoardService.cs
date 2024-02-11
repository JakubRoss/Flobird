﻿using Application.Model.Board;

namespace Application.Service
{
    public interface IBoardService
    {
        Task CreateBoardAsync(CreateBoardDto createBoardDto);
        Task<ResponseBoardDto> GetBoardAsync(int boardId);
        Task<List<ResponseBoardDto>> GetBoardsAsync();
        Task<List<ResponseBoardUser>> GetBoardUsersAsync(int boardId);
        Task DeleteBoardAsync(int boardId);
        Task AddUsersAsync(int boardId, int userId);
        Task UpdateBoardAsync(int boardId, UpdateBoardDto updateBoardDto);
        Task RemoveUserAsync(int boardId, int userId);
        Task SetUserRole(int boardId, int modifyUserId, int roles);
    }
}