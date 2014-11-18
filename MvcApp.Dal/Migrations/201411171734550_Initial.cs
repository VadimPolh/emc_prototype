namespace MvcApp.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Отделение",
                c => new
                    {
                        Id_отделения = c.Int(nullable: false, identity: true),
                        Наименование = c.String(),
                        EntityVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id_отделения);
            
            CreateTable(
                "dbo.Специальность",
                c => new
                    {
                        Id_специальности = c.Int(nullable: false, identity: true),
                        Название = c.String(),
                        Зав_отделением = c.String(),
                        EntityVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Id_отделения = c.Int(),
                    })
                .PrimaryKey(t => t.Id_специальности)
                .ForeignKey("dbo.Отделение", t => t.Id_отделения, cascadeDelete: true)
                .Index(t => t.Id_отделения);
            
            CreateTable(
                "dbo.Направление",
                c => new
                    {
                        Id_направления = c.Int(nullable: false, identity: true),
                        Наименование = c.String(),
                        EntityVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Id_специальности = c.Int(),
                    })
                .PrimaryKey(t => t.Id_направления)
                .ForeignKey("dbo.Специальность", t => t.Id_специальности, cascadeDelete: true)
                .Index(t => t.Id_специальности);
            
            CreateTable(
                "dbo.Курс",
                c => new
                    {
                        Id_курса = c.Int(nullable: false, identity: true),
                        Номер_курса = c.Int(nullable: false),
                        EntityVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Id_направления = c.Int(),
                    })
                .PrimaryKey(t => t.Id_курса)
                .ForeignKey("dbo.Направление", t => t.Id_направления, cascadeDelete: true)
                .Index(t => t.Id_направления);
            
            CreateTable(
                "dbo.Группа",
                c => new
                    {
                        Id_группы = c.Int(nullable: false, identity: true),
                        Номер_группы = c.String(),
                        EntityVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Id_курса = c.Int(),
                        Id_преподавателя = c.Int(),
                    })
                .PrimaryKey(t => t.Id_группы)
                .ForeignKey("dbo.Курс", t => t.Id_курса, cascadeDelete: true)
                .ForeignKey("dbo.Преподаватель", t => t.Id_преподавателя)
                .Index(t => t.Id_курса)
                .Index(t => t.Id_преподавателя);
            
            CreateTable(
                "dbo.Ячейка",
                c => new
                    {
                        Id_ячейки = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Номер_пары = c.Int(nullable: false),
                        Аудитория = c.String(),
                        EntityVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Id_группы = c.Int(),
                        Id_занятия = c.Int(),
                    })
                .PrimaryKey(t => t.Id_ячейки)
                .ForeignKey("dbo.Группа", t => t.Id_группы)
                .ForeignKey("dbo.Занятие", t => t.Id_занятия)
                .Index(t => t.Id_группы)
                .Index(t => t.Id_занятия);
            
            CreateTable(
                "dbo.Занятие",
                c => new
                    {
                        Id_занятия = c.Int(nullable: false, identity: true),
                        Наименование = c.String(),
                        EntityVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Id_вида_занятия = c.Int(),
                        Id_предмета = c.Int(),
                    })
                .PrimaryKey(t => t.Id_занятия)
                .ForeignKey("dbo.Вид_Занятия", t => t.Id_вида_занятия, cascadeDelete: true)
                .ForeignKey("dbo.Предмет", t => t.Id_предмета, cascadeDelete: true)
                .Index(t => t.Id_вида_занятия)
                .Index(t => t.Id_предмета);
            
            CreateTable(
                "dbo.Вид_Занятия",
                c => new
                    {
                        Id_вида_занятия = c.Int(nullable: false, identity: true),
                        Наименование = c.String(),
                        Тема = c.String(),
                        EntityVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id_вида_занятия);
            
            CreateTable(
                "dbo.Предмет",
                c => new
                    {
                        Id_предмета = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Учебник = c.String(),
                        План = c.String(),
                        ОКР = c.String(),
                        Тест = c.String(),
                        Количество_часов = c.Int(nullable: false),
                        EntityVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id_предмета);
            
            CreateTable(
                "dbo.Преподаватель",
                c => new
                    {
                        Id_преподавателя = c.Int(nullable: false, identity: true),
                        ФИО = c.String(),
                        Пароль = c.String(),
                        Стаж = c.Int(nullable: false),
                        EntityVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id_преподавателя);
            
            CreateTable(
                "dbo.Ученик",
                c => new
                    {
                        Id_ученика = c.Int(nullable: false, identity: true),
                        ФИО = c.String(),
                        Пароль = c.String(),
                        Почта = c.String(),
                        Возраст = c.Int(nullable: false),
                        EntityVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Id_группы = c.Int(),
                    })
                .PrimaryKey(t => t.Id_ученика)
                .ForeignKey("dbo.Группа", t => t.Id_группы, cascadeDelete: true)
                .Index(t => t.Id_группы);
            
            CreateTable(
                "dbo.преподаватель_занятие",
                c => new
                    {
                        id_преподавателя = c.Int(nullable: false),
                        id_занятия = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.id_преподавателя, t.id_занятия })
                .ForeignKey("dbo.Преподаватель", t => t.id_преподавателя, cascadeDelete: true)
                .ForeignKey("dbo.Занятие", t => t.id_занятия, cascadeDelete: true)
                .Index(t => t.id_преподавателя)
                .Index(t => t.id_занятия);
            
            CreateTable(
                "dbo.предмет_курс",
                c => new
                    {
                        Id_курса = c.Int(nullable: false),
                        Id_предмета = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id_курса, t.Id_предмета })
                .ForeignKey("dbo.Курс", t => t.Id_курса, cascadeDelete: true)
                .ForeignKey("dbo.Предмет", t => t.Id_предмета, cascadeDelete: true)
                .Index(t => t.Id_курса)
                .Index(t => t.Id_предмета);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Направление", "Id_специальности", "dbo.Специальность");
            DropForeignKey("dbo.предмет_курс", "Id_предмета", "dbo.Предмет");
            DropForeignKey("dbo.предмет_курс", "Id_курса", "dbo.Курс");
            DropForeignKey("dbo.Курс", "Id_направления", "dbo.Направление");
            DropForeignKey("dbo.Группа", "Id_преподавателя", "dbo.Преподаватель");
            DropForeignKey("dbo.Ученик", "Id_группы", "dbo.Группа");
            DropForeignKey("dbo.Группа", "Id_курса", "dbo.Курс");
            DropForeignKey("dbo.Ячейка", "Id_занятия", "dbo.Занятие");
            DropForeignKey("dbo.преподаватель_занятие", "id_занятия", "dbo.Занятие");
            DropForeignKey("dbo.преподаватель_занятие", "id_преподавателя", "dbo.Преподаватель");
            DropForeignKey("dbo.Занятие", "Id_предмета", "dbo.Предмет");
            DropForeignKey("dbo.Занятие", "Id_вида_занятия", "dbo.Вид_Занятия");
            DropForeignKey("dbo.Ячейка", "Id_группы", "dbo.Группа");
            DropForeignKey("dbo.Специальность", "Id_отделения", "dbo.Отделение");
            DropIndex("dbo.предмет_курс", new[] { "Id_предмета" });
            DropIndex("dbo.предмет_курс", new[] { "Id_курса" });
            DropIndex("dbo.преподаватель_занятие", new[] { "id_занятия" });
            DropIndex("dbo.преподаватель_занятие", new[] { "id_преподавателя" });
            DropIndex("dbo.Ученик", new[] { "Id_группы" });
            DropIndex("dbo.Занятие", new[] { "Id_предмета" });
            DropIndex("dbo.Занятие", new[] { "Id_вида_занятия" });
            DropIndex("dbo.Ячейка", new[] { "Id_занятия" });
            DropIndex("dbo.Ячейка", new[] { "Id_группы" });
            DropIndex("dbo.Группа", new[] { "Id_преподавателя" });
            DropIndex("dbo.Группа", new[] { "Id_курса" });
            DropIndex("dbo.Курс", new[] { "Id_направления" });
            DropIndex("dbo.Направление", new[] { "Id_специальности" });
            DropIndex("dbo.Специальность", new[] { "Id_отделения" });
            DropTable("dbo.предмет_курс");
            DropTable("dbo.преподаватель_занятие");
            DropTable("dbo.Ученик");
            DropTable("dbo.Преподаватель");
            DropTable("dbo.Предмет");
            DropTable("dbo.Вид_Занятия");
            DropTable("dbo.Занятие");
            DropTable("dbo.Ячейка");
            DropTable("dbo.Группа");
            DropTable("dbo.Курс");
            DropTable("dbo.Направление");
            DropTable("dbo.Специальность");
            DropTable("dbo.Отделение");
        }
    }
}
