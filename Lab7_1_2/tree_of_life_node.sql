CREATE TABLE [dbo].[tree_of_life_node] (
    [id]   INT           IDENTITY (1, 1) NOT NULL,
    [path] VARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
CREATE TABLE [dbo].[tree_of_life_materialized_path] (
    [node_id] INT NOT NULL,
    [path] NVARCHAR(200) NULL, 
    PRIMARY KEY CLUSTERED ([node_id] ASC),
    CONSTRAINT [tol_materialized_path_list_node_id] FOREIGN KEY ([node_id]) REFERENCES [dbo].[tree_of_life_node] ([id]) ON DELETE CASCADE
);
