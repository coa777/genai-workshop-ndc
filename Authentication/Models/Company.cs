namespace Authentication.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;

// Company entity. One company can have multiple addresses; addresses belong to a single company and cannot be shared.
public sealed record Company
{
    // Numeric auto-increment id. Thread-safe increment to avoid collisions in-memory.
    private static long s_nextId;
    public long Id { get; init; } = Interlocked.Increment(ref s_nextId);

    [Required]
    public string Name { get; init; } = null!;

    [Required]
    public string VatNumber { get; init; } = null!;

    // Stored addresses for this company. Addresses are owned by this company and cannot be shared.
    public List<PostalAddress> Addresses { get; private init; } = new();

    public Company() { }

    public Company(string name, string vatNumber)
    {
        Name = name;
        VatNumber = vatNumber;
    }

    // Adds a new postal address tied to this company.
    public PostalAddress AddAddress(string streetName, string zipCode, string number, string town, string country)
    {
        var addr = new PostalAddress(streetName, zipCode, number, town, country, Id);
        Addresses.Add(addr);
        return addr;
    }

    // Removes an address owned by this company. Returns true if removed.
    public bool RemoveAddress(long addressId)
    {
        var addr = Addresses.FirstOrDefault(a => a.Id == addressId && a.CompanyId == Id);
        if (addr is null)
            return false;

        return Addresses.Remove(addr);
    }
}
