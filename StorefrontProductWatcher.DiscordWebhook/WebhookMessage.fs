namespace StorefrontProductWatcher.DiscordWebhook

open System.Collections.Generic
open System.Text.Json.Serialization


type Author = {
    [<JsonPropertyName("name")>]
    Name: Option<string>
    [<JsonPropertyName("url")>]
    Url: Option<string>
    [<JsonPropertyName("icon_url")>]
    IconUrl: Option<string>
}

type Field = {
    [<JsonPropertyName("name")>]
    Name: string
    [<JsonPropertyName("value")>]
    Value: string
    [<JsonPropertyName("inline")>]
    Inline: Option<bool>
}

type Thumbnail = {
    [<JsonPropertyName("url")>]
    Url: string
}

type Image = {
    [<JsonPropertyName("url")>]
    Url: string
}

type Footer = {
    [<JsonPropertyName("text")>]
    Text: Option<string>
    [<JsonPropertyName("icon_url")>]
    IconUrl: Option<string>
}

type Embed = {
    [<JsonPropertyName("author")>]
    Author: Option<Author>
    [<JsonPropertyName("title")>]
    Title: Option<string>
    [<JsonPropertyName("url")>]
    Url: Option<string>
    [<JsonPropertyName("description")>]
    Description: Option<string>
    [<JsonPropertyName("color")>]
    Color: Option<string>
    [<JsonPropertyName("fields")>]
    Fields: Option<IEnumerable<Field>>
    [<JsonPropertyName("thumbnail")>]
    Thumbnail: Option<Thumbnail>
    [<JsonPropertyName("image")>]
    Image: Option<Image>
    [<JsonPropertyName("footer")>]
    Footer: Option<Footer>
}

type WebhookMessage = {
    [<JsonPropertyName("username")>]
    Username: Option<string>
    [<JsonPropertyName("avatar_url")>]
    AvatarUrl: Option<string>
    [<JsonPropertyName("content")>]
    Content: Option<string>
    [<JsonPropertyName("embeds")>]
    Embeds: Option<seq<Embed>>
}