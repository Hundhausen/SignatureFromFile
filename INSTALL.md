# Installation of SignatureFromFile

## Prerequisite

- Being able to run software with the .NET 4.8.1 Framework
- You need the Thunderbird Addon [Signature Switch from Achim Seufert](https://addons.thunderbird.net/de/thunderbird/addon/signature-switch)
- No other NativeMessagingHosts registered as signatureswitch (in the registry)

## Install

### Copy the files

Copy the 3 files (`SignatureFromFile.exe`, `SignatureFromFile.json` and `Newtonsoft.Json.dll`) into a folder.  
The Program can be installed for the whole machine or only the current user. Choose your installation path accordingly (rights to execute)

### Register the program

The `SignatureFromFile.json` needs to be registered as `signatureswitch` in the registry. When `signatureswitch` is already registered, this means you already have a different program installed. You can only have one program registered at the same time.

#### Current user

Create the key `[HKEY_CURRENT_USER\SOFTWARE\Mozilla\NativeMessagingHosts\signatureswitch]` and set the full path to the `SignatureFromFile.json` file.

#### Whole Machine

Create the key `[HKEY_LOCAL_MACHINE\SOFTWARE\Mozilla\NativeMessagingHosts\signatureswitch]` and set the full path to the `SignatureFromFile.json` file.

### Test the program

In the settings page of the Thunderbird plugin "Signature Switch" under "Native Messaging" you can enter the path to your signature file as the tag (eg. `"tag": "signature.html",`). The path can be a full path or a relative path (relative to "SignatureFromFile.exe").  
Be aware to use `/` or `\\` for the path.  
  
When it was successful, in the answer, the signature should contain your signature as message. `err` gets inserted when there was an error within the program. This could mean reading the signature file failed (wrong path). A different message could mean there was a problem starting the program.

### Use the program

In the Addon under "Signature" you can add signatures. As content you add the path to the signature surrounded by 2 underscores (eg. `__C:/Signatures/signature1.htm__`). Depending on your signature, you add this either to "Text" or "HTML".  
The signature is ready to use.

## Uninstall

### Remove the registry entry

Delete the registry key, depending how you installed it.

#### User

Delete the key `[HKEY_CURRENT_USER\SOFTWARE\Mozilla\NativeMessagingHosts\signatureswitch]`

#### Machine

Delete the key `[HKEY_LOCAL_MACHINE\SOFTWARE\Mozilla\NativeMessagingHosts\signatureswitch]`

### Delete the files

Delete the 3 files (`SignatureFromFile.exe`, `SignatureFromFile.json` and `Newtonsoft.Json.dll`).

### Delete or edit the signatures

Delete the signatures or edit your signatures in the Thunderbird Addon, so no keys (Text surrounded by 2 underscores on each side).

## Update

If there are updates, a tutorial will get added here.
