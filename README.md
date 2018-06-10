# HD.EFCore.Generator
efcore 代码生成器(含mysql和sql server）

解决的问题：
避免每次通过Scaffold-DbContext 命令从数据库reverse。
团队成员可以从一个公共的站点输入连接串和表名（可选）后reverse

原理：
通过efcore的源码找到Scaffold-DbContext命令的核心执行代码，从而提取出来，做成一个web站点
