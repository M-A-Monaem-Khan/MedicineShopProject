// wwwroot/js/encrypton.js

window.generateKey = async function generateKey() {

    const key = await crypto.subtle.generateKey(
        {
            name: "AES-GCM",
            length: 256,
        },
        true,
        ["encrypt", "decrypt"]
    );

    // Export the key as a raw format
    const exportedKey = await crypto.subtle.exportKey("raw", key);

    // Convert the exported key to Base64
    return btoa(String.fromCharCode(...new Uint8Array(exportedKey)));
}
window.encryptData = async function encryptData(keyBytes, data) {
    // Import the key
    const key = await crypto.subtle.importKey(
        "raw",
        new Uint8Array(keyBytes),
        {
            name: "AES-GCM",
            length: 256,
        },
        true,
        ["encrypt"]
    );

    const encoder = new TextEncoder();
    const encodedData = encoder.encode(data);
    const iv = crypto.getRandomValues(new Uint8Array(12)); // Initialization vector

    const encrypted = await crypto.subtle.encrypt(
        {
            name: "AES-GCM",
            iv: iv,
        },
        key,
        encodedData
    );

    //console.log( Array.from(new Uint8Array(encrypted)))
    // Convert arrays to JSON-serializable format
    return {
        iv: Array.from(iv), // Convert Uint8Array to Array
        encryptedData: Array.from(new Uint8Array(encrypted)) // Convert ArrayBuffer to Array
    };
}

window.decryptData = async function decryptData(key, iv, encryptedData) {
    try {
        const cryptoKey = await window.crypto.subtle.importKey(
            "raw",
            new Uint8Array(key),
            { name: "AES-GCM" },
            false,
            ["decrypt"]
        );

        const decrypted = await window.crypto.subtle.decrypt(
            {
                name: "AES-GCM",
                iv: new Uint8Array(iv)
            },
            cryptoKey,
            new Uint8Array(encryptedData)
        );

        return {
            decryptedData: Array.from(new Uint8Array(decrypted))
        };
    } catch (error) {
        console.error("Decryption failed:", error);
        return null;
    }
};

async function exportKey(key) {
    const rawKey = await crypto.subtle.exportKey("raw", key);
    return Array.from(new Uint8Array(rawKey));
}

async function importKey(rawKey) {
    //return await crypto.subtle.importKey(
    //    "raw",
    //    new Uint8Array(rawKey),
    //    {
    //        name: "AES-GCM",
    //    },
    //    true,
    //    ["encrypt", "decrypt"]
    //);
    const keyArray = new Uint8Array(rawKey);

    return keyArray;
}
