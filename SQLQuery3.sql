USE [CatKitchenDB]
GO
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([AuthorID], [AuthorName], [Login], [Password], [BirthDay], [Phone], [Stage], [Mail], [RoleID]) VALUES (1, N'Ахиллес Про', N'Ahill', N'123', CAST(N'2007-08-22T00:00:00.000' AS DateTime), N'+79603354476', CAST(0.000 AS Decimal(18, 3)), N'hgjfg@mail.com', 1)
INSERT [dbo].[Authors] ([AuthorID], [AuthorName], [Login], [Password], [BirthDay], [Phone], [Stage], [Mail], [RoleID]) VALUES (2, N'root', N'1', N'1', CAST(N'2007-08-22T00:00:00.000' AS DateTime), N'+79603354476', CAST(0.000 AS Decimal(18, 3)), N'hgjfg@mail.com', 1)
INSERT [dbo].[Authors] ([AuthorID], [AuthorName], [Login], [Password], [BirthDay], [Phone], [Stage], [Mail], [RoleID]) VALUES (3, N'Барсик Мастер', N'barscic', N'12', CAST(N'2007-08-22T00:00:00.000' AS DateTime), N'+79603354476', CAST(0.000 AS Decimal(18, 3)), N'hgjfg@mail.com', 2)
INSERT [dbo].[Authors] ([AuthorID], [AuthorName], [Login], [Password], [BirthDay], [Phone], [Stage], [Mail], [RoleID]) VALUES (4, N'Мурзик', N'mursic', N'124', CAST(N'2007-08-22T00:00:00.000' AS DateTime), N'+79603354476', CAST(0.000 AS Decimal(18, 3)), N'hgjfg@mail.com', 2)
INSERT [dbo].[Authors] ([AuthorID], [AuthorName], [Login], [Password], [BirthDay], [Phone], [Stage], [Mail], [RoleID]) VALUES (5, N'Кисель Повар', N'kisel228', N'***', CAST(N'2007-08-22T00:00:00.000' AS DateTime), N'+79603354476', CAST(0.000 AS Decimal(18, 3)), N'hgjfg@mail.com', 3)
INSERT [dbo].[Authors] ([AuthorID], [AuthorName], [Login], [Password], [BirthDay], [Phone], [Stage], [Mail], [RoleID]) VALUES (8, N'root', N'root', N'root', CAST(N'2007-08-22T00:00:00.000' AS DateTime), N'+79603354476', CAST(0.000 AS Decimal(18, 3)), N'hgjfg@mail.com', 1)
SET IDENTITY_INSERT [dbo].[Authors] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (1, N'Рыбные деликатесы')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (2, N'Мясные угощения')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (3, N'Молочные лакомтсва')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (4, N'Птичьи блюда')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (5, N'Закуски и салаты')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (6, N'Десерты для котов')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (7, N'Блюда для привередливых')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (8, N'Ветеринарные блюда')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
INSERT [dbo].[Recipes] ([RecipeID], [RecipeName], [Description], [CategoryID], [AuthorID], [CookingTime], [Image]) VALUES (1, N'Форель на углях - классика котофейства', N'Деликатная форель, приготовленная на открытом огне с минимальными специями', 1, 1, 45, N'/images/fortel.jpg')
INSERT [dbo].[Recipes] ([RecipeID], [RecipeName], [Description], [CategoryID], [AuthorID], [CookingTime], [Image]) VALUES (2, N'Паштет из куриной печени - высший класс', N'Нежный паштет, любимый всеми котами, с кремовой текстурой', 2, 2, 30, N'/images/pashtet.jpg')
INSERT [dbo].[Recipes] ([RecipeID], [RecipeName], [Description], [CategoryID], [AuthorID], [CookingTime], [Image]) VALUES (3, N'Молочный суп с сливками', N'Теплый и нежный суп, идеален для ленивого полудня', 3, 3, 20, N'/images/soup.jpg')
INSERT [dbo].[Recipes] ([RecipeID], [RecipeName], [Description], [CategoryID], [AuthorID], [CookingTime], [Image]) VALUES (4, N'Тунец гриль по-кошачьему', N'Заморский тунец с ароматом моря, приготовленный в совершенстве', 1, 4, 50, N'/images/tunec.jpg')
INSERT [dbo].[Recipes] ([RecipeID], [RecipeName], [Description], [CategoryID], [AuthorID], [CookingTime], [Image]) VALUES (5, N'Мясное ассорти охотника', N'Смесь мяса кролика, говядины и птицы для настоящих охотников', 2, 5, 60, N'/images/assort.jpg')
INSERT [dbo].[Recipes] ([RecipeID], [RecipeName], [Description], [CategoryID], [AuthorID], [CookingTime], [Image]) VALUES (6, N'Омлет из перепелиных яиц', N'Легкий и питательный омлет для завтрака истинного кота', 3, 1, 15, N'/images/omlet.jpg')
INSERT [dbo].[Recipes] ([RecipeID], [RecipeName], [Description], [CategoryID], [AuthorID], [CookingTime], [Image]) VALUES (7, N'Рыбный паштет "Морской закат"', N'Смесь тунца и форели с нежной текстурой', 1, 2, 25, N'/images/ryb_pashtet.jpg')
INSERT [dbo].[Recipes] ([RecipeID], [RecipeName], [Description], [CategoryID], [AuthorID], [CookingTime], [Image]) VALUES (8, N'Мясные шарики с кроликом', N'Питательные шарики из мяса кролика, идеальны для перекуса', 2, 3, 40, N'/images/shariki.jpg')
INSERT [dbo].[Recipes] ([RecipeID], [RecipeName], [Description], [CategoryID], [AuthorID], [CookingTime], [Image]) VALUES (9, N'Творожная запеканка', N'Полезная и вкусная запеканка из творога для развивающегося котенка', 3, 4, 35, N'/images/zapekanka.jpg')
INSERT [dbo].[Recipes] ([RecipeID], [RecipeName], [Description], [CategoryID], [AuthorID], [CookingTime], [Image]) VALUES (10, N'Стейк из говядины по-аристократично', N'Редкий стейк для котов с утонченным вкусом', 2, 5, 55, N'/images/steak.jpg')
GO
SET IDENTITY_INSERT [dbo].[Tags] ON 

INSERT [dbo].[Tags] ([TagID], [TagName]) VALUES (1, N'Быстро                                            ')
INSERT [dbo].[Tags] ([TagID], [TagName]) VALUES (2, N'Легко                                             ')
INSERT [dbo].[Tags] ([TagID], [TagName]) VALUES (3, N'Полезно                                           ')
INSERT [dbo].[Tags] ([TagID], [TagName]) VALUES (4, N'Праздник                                          ')
INSERT [dbo].[Tags] ([TagID], [TagName]) VALUES (5, N'Популярно                                         ')
INSERT [dbo].[Tags] ([TagID], [TagName]) VALUES (6, N'Низкокалорийно                                    ')
INSERT [dbo].[Tags] ([TagID], [TagName]) VALUES (7, N'Для активных                                      ')
INSERT [dbo].[Tags] ([TagID], [TagName]) VALUES (8, N'Экзотика                                          ')
INSERT [dbo].[Tags] ([TagID], [TagName]) VALUES (9, N'Азия                                              ')
INSERT [dbo].[Tags] ([TagID], [TagName]) VALUES (10, N'Европа                                            ')
SET IDENTITY_INSERT [dbo].[Tags] OFF
GO
