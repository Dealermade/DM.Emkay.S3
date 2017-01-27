DM.Emkay.S3
========

This library is a wrapper for Amazon S3.  

It provides the ability to upload files and folders (including subfolders), list buckets and folders, and delete buckets and files.  

This package also includes MSBuild tasks to perform these functions.

## Download

The `DM.Emkay.S3` library is available on nuget.org.

To install it, run the following command in the Package Manager Console:

    PM> Install-Package DM.Emkay.S3

More information about the NuGet package is avaliable at [https://nuget.org/packages/DM.Emkay.S3](https://nuget.org/packages/DM.Emkay.S3)

## Getting Started

In order to use the tasks in your project, you need to import the `DM.Emkay.S3.Tasks.targets` file.

You need to adjust the path to your needs.

    <Import Project="..\packages\DM.Emkay.S3\1.0.0.3\lib\net45\DM.Emkay.S3.Tasks.targets" />

You may also reference individual tasks by loading the `DM.Emkay.S3.dll` assembly file itself.

    <PropertyGroup>
        <EmkayS3ClassLibrary>..\packages\DM.Emkay.S3.1.0.0.3\lib\net45\DM.Emkay.S3.dll</EmkayS3ClassLibrary>
    </PropertyGroup>
    <UsingTask AssemblyFile="$(EmkayS3ClassLibrary)" TaskName="PublishFolder" />
    <UsingTask AssemblyFile="$(EmkayS3ClassLibrary)" TaskName="PublishFiles" />

### Tasks

[DeleteBucket](#delete-bucket)  
[DeleteChildren](#delete-children)  
[EnumerateBuckets](#enumerate-buckets)  
[EnumerateChildren](#enumerate-children)  
[PublishFiles](#publish-files)  
[PublishFilesWithHeaders](#publish-files)  

#### Required parameters for all tasks

**Key**

The AWS Access Key ID.

**Secret**

The AWS Secret Access Key.

**Bucket**

The name of the AWS Bucket.

#### Optional parameters for all tasks

**Region**

The default AWS Region Endpoint is "us-east-1", but any of the [AWS S3 region names](http://docs.aws.amazon.com/general/latest/gr/rande.html#s3_region) can be used.

**TimeoutMilliseconds**

The number of milliseconds before a request automatically times out.

The default value is 300000 which is 5 minutes.

The maximum value is 2147483647.

**BufferSizeKilobytes**

The size of the buffer to use when publishing files

The default value is 8192 which is 8KB.

The maximum value is 2147483647.


## Delete Bucket

Deletes a bucket.

### Example

    <DeleteBucket
        Key="$(Key)"
        Secret="$(Secret)"
        Bucket="$(Bucket)" />

## Delete Children

Deletes one or more objects within a bucket.

### Example

    <DeleteChildren
        Key="$(Key)"
        Secret="$(Secret)"
        Bucket="$(Bucket)"
        Children="Folder1;Folder2\SubFolder1" />

## Enumerate Buckets

Lists all the buckets.

### Example

    <EnumerateBuckets
        Key="$(Key)"
        Secret="$(Secret)"
        Bucket="$(Bucket)">
      <Output TaskParameter="Buckets" PropertyName="EnumeratedBuckets"/>
    </EnumerateBuckets>

    <Message Text="List of buckets $(EnumeratedBuckets)"/>

## Enumerate Children

Lists all the object within a bucket.

### Optional Parameters

**Prefix**

Limits the response to keys that begin with the specified prefix.
This can be used to list objects in a specific subfolders for example.

### Example

    <EnumerateChildren
        Key="$(Key)"
        Secret="$(Secret)"
        Bucket="$(Bucket)"
        Prefix="find me">
      <Output TaskParameter="Children" PropertyName="EnumeratedChildren"/>
    </EnumerateBuckets>

    <Message Text="List of children $(EnumeratedChildren)"/>

## Publish Files

Uploads files to a bucket.

By default the files will be publicly readable.

### Required Parameters

**SourceFiles**

One or more files to upload.

**DestinationFolder**

The destination folder to upload the files to.

### Optional Parameters

**UploadIfNotExists**

A regular expression of file names to upload only if they don't already exist.

**PublicRead**

A boolean value indicating whether or not to set the ACL on the objects to allow read access to anonymous (unauthenticated) users or only if the owner is granted acess.

The default is `True`, which sets the ACL on the objects to allow anonymous users read access.

### Example 1

    <PublishFiles
        Key="$(Key)"
        Secret="$(Secret)"
        Bucket="$(Bucket)"
        SourceFiles="README.md;DM.Emkay.S3.nupkg"
        UploadIfNotExists="*.nupkg"
        DestinationFolder="Folder1/Folder2" />

### Example 2

    <ItemGroup>
        <Directories Include="css;scripts;">
            <Content-Type>text/css</Content-Type>
            <Content-Encoding>gzip</Content-Encoding>
        </Directories>
    </ItemGroup> 

    <PublishFilesWithHeaders
        Key="$(Key)"
        Secret="$(Secret)"
        Bucket="$(Bucket)"
        Region="us-west-1"
        SourceFiles="@(Directories)"
        DestinationFolder="Folder1/Folder2" />

## License

The source code is available under the [MIT license](http://opensource.org/licenses/mit-license.php).

