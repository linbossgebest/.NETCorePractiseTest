using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Sample03.Models
{
    public partial class cmsContext : DbContext
    {
        public cmsContext()
        {
        }

        public cmsContext(DbContextOptions<cmsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Articlecategory> Articlecategory { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Content> Content { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<Managerlog> Managerlog { get; set; }
        public virtual DbSet<Managerrole> Managerrole { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Rolepermission> Rolepermission { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;userid=root;pwd=root;port=3306;database=cms;sslmode=none;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AddManagerId)
                    .HasColumnType("int(11)")
                    .HasComment("添加人ID");

                entity.Property(e => e.AddTime).HasComment("添加时间");

                entity.Property(e => e.Author)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("作者");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasComment("分类ID");

                entity.Property(e => e.Content).HasComment("文章内容");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("图片地址");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("int(11)")
                    .HasComment("是否删除");

                entity.Property(e => e.IsPublish)
                    .HasColumnType("int(11)")
                    .HasComment("是否发布");

                entity.Property(e => e.IsRed).HasColumnType("int(11)");

                entity.Property(e => e.IsSlide)
                    .HasColumnType("int(11)")
                    .HasComment("是否轮播显示");

                entity.Property(e => e.IsTop)
                    .HasColumnType("int(11)")
                    .HasComment("是否置顶");

                entity.Property(e => e.ModifyManagerId)
                    .HasColumnType("int(11)")
                    .HasComment("修改人ID");

                entity.Property(e => e.ModifyTime).HasComment("修改时间");

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("SEO描述");

                entity.Property(e => e.SeoKeyword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("SEO关键字");

                entity.Property(e => e.SeoTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("SEO标题");

                entity.Property(e => e.Sort)
                    .HasColumnType("int(11)")
                    .HasComment("排序");

                entity.Property(e => e.Source)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("来源");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("文章标题");

                entity.Property(e => e.ViewCount)
                    .HasColumnType("int(11)")
                    .HasComment("浏览次数");
            });

            modelBuilder.Entity<Articlecategory>(entity =>
            {
                entity.ToTable("articlecategory");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ClassLayer)
                    .HasColumnType("int(11)")
                    .HasComment("类别深度");

                entity.Property(e => e.ClassList)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("类别ID列表(逗号分隔开)");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("分类图标");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("int(11)")
                    .HasComment("是否删除");

                entity.Property(e => e.ParentId)
                    .HasColumnType("int(11)")
                    .HasComment("父分类ID");

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("分类SEO描述");

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("分类SEO关键字");

                entity.Property(e => e.SeoTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("分类SEO标题");

                entity.Property(e => e.Sort)
                    .HasColumnType("int(11)")
                    .HasComment("排序");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("分类标题");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime).HasColumnName("add_time");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ContentId)
                    .HasColumnName("content_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.ToTable("content");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime).HasColumnName("add_time");

                entity.Property(e => e.Content1)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyTime).HasColumnName("modify_time");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("manager");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AddManagerId)
                    .HasColumnType("int(11)")
                    .HasComment("添加人");

                entity.Property(e => e.AddTime).HasComment("添加时间");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("头像");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("邮箱地址");

                entity.Property(e => e.IsDelete)
                    .HasColumnType("int(11)")
                    .HasComment("是否删除");

                entity.Property(e => e.IsLock)
                    .HasColumnType("int(11)")
                    .HasComment("是否锁定");

                entity.Property(e => e.LoginCount)
                    .HasColumnType("int(11)")
                    .HasComment("登录次数");

                entity.Property(e => e.LoginLastIp)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("最后一次登录IP");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasComment("手机号码");

                entity.Property(e => e.ModifyManagerId)
                    .HasColumnType("int(11)")
                    .HasComment("修改人");

                entity.Property(e => e.ModifyTime).HasComment("修改时间");

                entity.Property(e => e.NickName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasComment("用户昵称");

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("密码");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("备注");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasComment("角色ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasComment("用户名");
            });

            modelBuilder.Entity<Managerlog>(entity =>
            {
                entity.ToTable("managerlog");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ActionType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("操作类型");

                entity.Property(e => e.AddIp)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("操作IP");

                entity.Property(e => e.AddManageId)
                    .HasColumnType("int(11)")
                    .HasComment("操作人ID");

                entity.Property(e => e.AddManagerNickName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("操作人名称");

                entity.Property(e => e.AddTime).HasComment("操作时间");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("备注");
            });

            modelBuilder.Entity<Managerrole>(entity =>
            {
                entity.ToTable("managerrole");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AddManagerId)
                    .HasColumnType("int(11)")
                    .HasComment("添加人");

                entity.Property(e => e.AddTime).HasComment("添加时间");

                entity.Property(e => e.IsDelete)
                    .HasColumnType("int(11)")
                    .HasComment("是否删除");

                entity.Property(e => e.IsSystem)
                    .HasColumnType("int(11)")
                    .HasComment("是否系统默认");

                entity.Property(e => e.ModifyManagerId)
                    .HasColumnType("int(11)")
                    .HasComment("修改人");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("备注");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("角色名称");

                entity.Property(e => e.RoleType)
                    .HasColumnType("int(11)")
                    .HasComment("角色类型1超管2系管");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AddManagerId)
                    .HasColumnType("int(11)")
                    .HasComment("添加人");

                entity.Property(e => e.AddTime).HasComment("添加时间");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("显示名称");

                entity.Property(e => e.IconUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("图标地址");

                entity.Property(e => e.IsDelete)
                    .HasColumnType("int(11)")
                    .HasComment("是否删除");

                entity.Property(e => e.IsDisplay)
                    .HasColumnType("int(11)")
                    .HasComment("是否显示");

                entity.Property(e => e.IsSystem)
                    .HasColumnType("int(11)")
                    .HasComment("是否系统默认");

                entity.Property(e => e.LinkUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("链接地址");

                entity.Property(e => e.ModifyManagerId)
                    .HasColumnType("int(11)")
                    .HasComment("修改人");

                entity.Property(e => e.ModifyTime).HasComment("修改时间");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("Name");

                entity.Property(e => e.ParentId)
                    .HasColumnType("int(11)")
                    .HasComment("父菜单ID");

                entity.Property(e => e.Permission)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("操作权限（按钮权限时使用）");

                entity.Property(e => e.Sort)
                    .HasColumnType("int(11)")
                    .HasComment("排序数字");
            });

            modelBuilder.Entity<Rolepermission>(entity =>
            {
                entity.ToTable("rolepermission");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.MenuId)
                    .HasColumnType("int(11)")
                    .HasComment("菜单主键");

                entity.Property(e => e.Permission)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("操作类型（功能权限）");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasComment("角色主键");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
