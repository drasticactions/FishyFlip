// <copyright file="ATCidTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;

namespace FishyFlip.Tests;

/// <summary>
/// Tests for ATCid functionality.
/// </summary>
[TestClass]
public class ATCidTests
{
    /// <summary>
    /// Test CIDv1 decoding and encoding.
    /// </summary>
    [TestMethod]
    public void ATCid_CIDv1_DecodeAndEncode_ShouldWork()
    {
        // Arrange
        const string cidv1String = "bafybeigdyrzt5sfp7udm7hu76uh7y26nf3efuylqabf3oclgtqy55fbzdi";

        // Act
        var cid = ATCid.Decode(cidv1String);
        var encodedString = cid.ToString();

        // Assert
        Assert.AreEqual(1, cid.Version);
        Assert.IsNotNull(cid.Hash);
        Assert.AreEqual(0x70UL, cid.Codec); // dag-pb
        Assert.AreEqual(cidv1String, encodedString);
    }

    /// <summary>
    /// Test reading CID from bytes.
    /// </summary>
    [TestMethod]
    public void ATCid_ReadFromBytes_ShouldWork()
    {
        // Arrange
        var testBytes = new byte[] { 0x01, 0x70, 0x12, 0x20, 0x00, 0x01, 0x02, 0x03 };

        // Act
        var cid = ATCid.Read(testBytes);

        // Assert
        Assert.AreEqual(1, cid.Version);
        Assert.AreEqual(0x70UL, cid.Codec);
        Assert.IsNotNull(cid.Hash);
    }

    /// <summary>
    /// Test CID equality comparison.
    /// </summary>
    [TestMethod]
    public void ATCid_Equality_ShouldWork()
    {
        // Arrange
        const string cidString = "QmYjtig7VJQ6XsnUjqqJvj7QaMcCAwtrgNdahSiFofrE7o";
        var cid1 = ATCid.Decode(cidString);
        var cid2 = ATCid.Decode(cidString);
        var cid3 = new ATCid(0, 0x70, new byte[] { 1, 2, 3 });

        // Act & Assert
        Assert.AreEqual(cid1, cid2);
        Assert.IsTrue(cid1.Equals(cid2));
        Assert.AreNotEqual(cid1, cid3);
        Assert.IsFalse(cid1.Equals(cid3));
        Assert.IsFalse(cid1.Equals(null));
    }

    /// <summary>
    /// Test CID hash code generation.
    /// </summary>
    [TestMethod]
    public void ATCid_GetHashCode_ShouldWork()
    {
        // Arrange
        const string cidString = "QmYjtig7VJQ6XsnUjqqJvj7QaMcCAwtrgNdahSiFofrE7o";
        var cid1 = ATCid.Decode(cidString);
        var cid2 = ATCid.Decode(cidString);
        var cid3 = new ATCid(0, 0x70, new byte[] { 1, 2, 3 });

        // Act
        var hash1 = cid1.GetHashCode();
        var hash2 = cid2.GetHashCode();
        var hash3 = cid3.GetHashCode();

        // Assert
        Assert.AreEqual(hash1, hash2);
        Assert.AreNotEqual(hash1, hash3);
    }

    /// <summary>
    /// Test CID to bytes conversion.
    /// </summary>
    [TestMethod]
    public void ATCid_ToBytes_ShouldWork()
    {
        // Arrange
        var testHash = new byte[] { 0x12, 0x20, 0x00, 0x01, 0x02, 0x03 };
        var cid = new ATCid(1, 0x70, testHash);

        // Act
        var bytes = cid.ToBytes();

        // Assert
        Assert.IsNotNull(bytes);
        Assert.IsTrue(bytes.Length > 0);
        Assert.AreEqual(1, bytes[0]); // Version
    }

    /// <summary>
    /// Test CID constructor validation.
    /// </summary>
    [TestMethod]
    public void ATCid_Constructor_WithNullHash_ShouldThrow()
    {
        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => new ATCid(1, 0x70, null!));
    }

    /// <summary>
    /// Test CID decode with invalid string.
    /// </summary>
    [TestMethod]
    public void ATCid_Decode_WithInvalidString_ShouldThrow()
    {
        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => ATCid.Decode(string.Empty));
        Assert.ThrowsException<ArgumentException>(() => ATCid.Decode("invalid"));
    }

    /// <summary>
    /// Test CID read with null or empty bytes.
    /// </summary>
    [TestMethod]
    public void ATCid_Read_WithInvalidBytes_ShouldThrow()
    {
        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => ATCid.Read(null!));
        Assert.ThrowsException<ArgumentException>(() => ATCid.Read(Array.Empty<byte>()));
    }

    /// <summary>
    /// Test CID with unsupported version.
    /// </summary>
    [TestMethod]
    public void ATCid_Read_WithUnsupportedVersion_ShouldThrow()
    {
        // Arrange
        var invalidBytes = new byte[] { 0x99 }; // Unsupported version

        // Act & Assert
        Assert.ThrowsException<NotSupportedException>(() => ATCid.Read(invalidBytes));
    }

    /// <summary>
    /// Test CIDv0 specific properties.
    /// </summary>
    [TestMethod]
    public void ATCid_CIDv0_Properties_ShouldBeCorrect()
    {
        // Arrange
        const string cidv0String = "QmYjtig7VJQ6XsnUjqqJvj7QaMcCAwtrgNdahSiFofrE7o";

        // Act
        var cid = ATCid.Decode(cidv0String);

        // Assert
        Assert.AreEqual(0, cid.Version);
        Assert.AreEqual(0x70UL, cid.Codec); // dag-pb codec for CIDv0
        Assert.IsNotNull(cid.Hash);
        Assert.IsTrue(cid.Hash.Length > 0);
    }

    /// <summary>
    /// Test that different CIDs produce different hash codes.
    /// </summary>
    [TestMethod]
    public void ATCid_DifferentCids_ShouldHaveDifferentHashCodes()
    {
        // Arrange
        var cid1 = new ATCid(0, 0x70, new byte[] { 1, 2, 3 });
        var cid2 = new ATCid(1, 0x70, new byte[] { 1, 2, 3 });
        var cid3 = new ATCid(0, 0x71, new byte[] { 1, 2, 3 });
        var cid4 = new ATCid(0, 0x70, new byte[] { 4, 5, 6 });

        // Act
        var hash1 = cid1.GetHashCode();
        var hash2 = cid2.GetHashCode();
        var hash3 = cid3.GetHashCode();
        var hash4 = cid4.GetHashCode();

        // Assert - While hash codes can theoretically collide, these should be different
        Assert.AreNotEqual(hash1, hash2);
        Assert.AreNotEqual(hash1, hash3);
        Assert.AreNotEqual(hash1, hash4);
    }

    /// <summary>
    /// Test CID round-trip conversion.
    /// </summary>
    [TestMethod]
    public void ATCid_RoundTrip_ShouldPreserveData()
    {
        // Arrange
        var originalHash = new byte[] { 0x12, 0x20, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
        var originalCid = new ATCid(1, 0x70, originalHash);

        // Act
        var bytes = originalCid.ToBytes();
        var reconstructedCid = ATCid.Read(bytes);

        // Assert
        Assert.AreEqual(originalCid.Version, reconstructedCid.Version);
        Assert.AreEqual(originalCid.Codec, reconstructedCid.Codec);
        CollectionAssert.AreEqual(originalCid.Hash, reconstructedCid.Hash);
        Assert.AreEqual(originalCid, reconstructedCid);
    }

    /// <summary>
    /// Test CID string representation for different versions.
    /// </summary>
    [TestMethod]
    public void ATCid_ToString_ShouldFormatCorrectly()
    {
        // Arrange & Act
        var cidv0 = new ATCid(0, 0x70, new byte[] { 0x12, 0x20, 0x01, 0x02, 0x03 });
        var cidv1 = new ATCid(1, 0x70, new byte[] { 0x12, 0x20, 0x01, 0x02, 0x03 });

        var stringv0 = cidv0.ToString();
        var stringv1 = cidv1.ToString();

        // Assert
        Assert.IsNotNull(stringv0);
        Assert.IsNotNull(stringv1);
        Assert.IsTrue(stringv0.Length > 0);
        Assert.IsTrue(stringv1.Length > 0);
        Assert.AreNotEqual(stringv0, stringv1);

        // CIDv1 should start with 'b' (base32 multibase)
        Assert.AreEqual('b', stringv1[0]);
    }

    /// <summary>
    /// Test object equality override.
    /// </summary>
    [TestMethod]
    public void ATCid_ObjectEquals_ShouldWork()
    {
        // Arrange
        var cid = new ATCid(0, 0x70, new byte[] { 1, 2, 3 });
        object cidAsObject = new ATCid(0, 0x70, new byte[] { 1, 2, 3 });
        object differentObject = "not a cid";

        // Act & Assert
        Assert.IsTrue(cid.Equals(cidAsObject));
        Assert.IsFalse(cid.Equals(differentObject));
        Assert.IsFalse(cid.Equals((object?)null));
    }
}