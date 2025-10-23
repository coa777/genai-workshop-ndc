namespace Authentication.Models;

using System.ComponentModel.DataAnnotations;
using System.Threading;

// Postal address owned by a single company. Addresses cannot be shared between companies.
public sealed record PostalAddress
{
	// Numeric auto-increment id. Thread-safe increment to avoid collisions in-memory.
	private static long s_nextId;
	public long Id { get; init; } = Interlocked.Increment(ref s_nextId);

	[Required]
	public string StreetName { get; init; } = null!;

	[Required]
	public string ZipCode { get; init; } = null!;

	[Required]
	public string Number { get; init; } = null!;

	[Required]
	public string Town { get; init; } = null!;

	[Required]
	public string Country { get; init; } = null!;

	// Owner company id — ties address to exactly one company
	public long CompanyId { get; init; }

	public PostalAddress() { }

	public PostalAddress(string streetName, string zipCode, string number, string town, string country, long companyId) =>
		(StreetName, ZipCode, Number, Town, Country, CompanyId) = (streetName, zipCode, number, town, country, companyId);
}
