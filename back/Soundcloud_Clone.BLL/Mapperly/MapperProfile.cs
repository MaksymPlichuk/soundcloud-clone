using Riok.Mapperly.Abstractions;
using Soundcloud_Clone.BLL.Dtos.Album;
using Soundcloud_Clone.BLL.Dtos.Auth;
using Soundcloud_Clone.BLL.Dtos.Comment;
using Soundcloud_Clone.BLL.Dtos.Song;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Enitites.Identity;


namespace Soundcloud_Clone.BLL.Mapperly
{
    [Mapper(UseDeepCloning = true, RequiredMappingStrategy = RequiredMappingStrategy.Source)]
    public partial class MapperProfile
    {
        public partial CommentDto CommentToDto(CommentEntity entity);
        public partial List<CommentDto> ListCommentsToDto(List<CommentEntity> entities);
        public partial CommentEntity CommentDtoToEntity(CommentDto dto);

        public partial CommentEntity CreateCommentToEntity(CreateCommentDto dto);
        public partial void UpdateComment(UpdateCommentDto dto, [MappingTarget] CommentEntity entity);


        public partial UserDto UserToDto(UserEntity entity);
        public partial UserForInfoDto UserForInfoToDto(UserEntity entity);
        public partial UserEntity RegisterDtoToUser(RegisterDto dto);


        public partial SongDto SongToDto(SongEntity entity);
        public partial List<SongDto> ListSongsToDto(List<SongEntity> entities);
        public partial SongEntity SongDtoToEntity(SongDto dto);
        public partial SongEntity CreateSongToEntity(CreateSongDto dto);
        public partial void UpdateSong(UpdateSongDto dto, [MappingTarget] SongEntity entity);


        public partial AlbumDto AlbumToDto(AlbumEntity entity);
        public partial List<AlbumDto> ListAlbumsToDto(List<AlbumEntity> entities);
        public partial AlbumEntity AlbumDtoToEntity(AlbumDto dto);
        [MapperIgnoreTarget(nameof(AlbumEntity.Image))]
        [MapperIgnoreTarget(nameof(AlbumEntity.Songs))]
        public partial AlbumEntity CreateAlbumToEntity(CreateAlbumDto dto);
        [MapperIgnoreTarget(nameof(AlbumEntity.Image))]
        [MapperIgnoreTarget(nameof(AlbumEntity.Songs))]
        public partial void UpdateAlbum(UpdateAlbumDto dto,[MappingTarget] AlbumEntity entity);


    }
}
