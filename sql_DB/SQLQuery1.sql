CREATE TABLE [dbo].[movies](
	[MovieId] [int] IDENTITY(1,1),
	[MovieName] [nvarchar](500),
	[MovieRating] [int] ,
	[MovieCategory] [nvarchar](500)

)

use abcMovies

ALTER TABLE [dbo].[movies]
ADD [PhotoFileName] [nvarchar](500);

INSERT into [dbo].[movies] ([MovieName],[MovieRating],[MovieCategory],[PhotoFileName]) VALUES ('Bambi',5,'Disney','anonymos.png')

INSERT into [dbo].[movies] ([MovieName],[MovieRating],[MovieCategory]) VALUES ('micky mouse',3,'Disney')



select *
from [dbo].[movies]
where MovieCategory ='Disney'

insert into dbo.movies
(MovieName,MovieRating,MovieCategory,PhotoFileName)
  values (@MovieName,@MovieRating,@MovieCategory,@PhotoFileName)

 select Top 10 MovieId,MovieName, MovieRating, MovieCategory,PhotoFileName
from dbo.movies
where MovieCategory ='Disney'
order by MovieRating desc

select * FROM movies where
MovieId = 2000

select *
from [dbo].[movies]

DELETE FROM [dbo].[movies]
where MovieId =2008

select Top 10 MovieId,MovieName, MovieRating, MovieCategory,PhotoFileName
                            from dbo.movies
                            order by MovieRating desc