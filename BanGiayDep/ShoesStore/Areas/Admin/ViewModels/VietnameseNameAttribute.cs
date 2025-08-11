using System;

[AttributeUsage(AttributeTargets.Field)]
public class VietnameseNameAttribute : Attribute
{
    public string Name { get; set; }
    public VietnameseNameAttribute(string name)
    {
        Name = name;
    }
}