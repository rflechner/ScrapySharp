namespace ScrapySharp.Core

    
    open System
    open System.IO
    open System.Net
    open System.Runtime.Serialization.Formatters.Binary
    open System.Text
    
    type CssSelectorTokenizer() =
        let mutable charCount:int = 0
        let mutable source = List<char>.Empty
        let mutable cssSelector = ""
        let mutable inQuotes:bool = false

        let getOffset (t:List<char>) = 
                charCount - 1 - t.Length

        member public x.Tokenize(pCssSelector:string) =
            cssSelector <- pCssSelector
            source <- Array.toList(cssSelector.ToCharArray())
            charCount <- source.Length
            x.tokenize() |> List.toArray
            
        member private x.tokenize() = 
            let rec readString acc = function
                | c :: t when Char.IsLetterOrDigit(c) || c.Equals('-') || c.Equals('_') 
                    || c.Equals('+') || c.Equals('/')
                     -> readString (acc + (c.ToString())) t
                | '\'' :: t -> 
                    if inQuotes then
                        inQuotes <- false
                        acc, t
                    else
                        inQuotes <- true
                        readString acc t
            
                | '\\' :: '\'' :: t when inQuotes ->
                    readString (acc + ('\''.ToString())) t

                | c :: t when inQuotes ->
                    readString (acc + (c.ToString())) t
                | c :: t -> acc, c :: t
                | [] -> 
                    acc, []
                | _ ->
                    failwith "Invalid css selector syntax"
        
            let (|TokenStr|_|) (s:string) x  =
                let chars = Seq.toList s

                let rec equal x s =
                    match x, s with
                    | x, [] -> Some(x)
                    | xh :: xt, sh :: st when xh = sh -> equal xt st
                    | _ -> None

                equal x chars

            let rec tokenize' acc sourceChars = 
                match sourceChars with
                | w :: t when Char.IsWhiteSpace(w) -> 
                    let seqtoken = (acc |> List.toSeq |> Seq.skip(1) |> Seq.toList)
                    match acc.Head with
                        | Token.Ancestor(o) -> tokenize' (Token.Ancestor(getOffset(t)) :: seqtoken) t
                        | Token.AllChildren(o) -> tokenize' (Token.AllChildren(getOffset(t)) :: seqtoken) t
                        | Token.DirectChildren(o) -> tokenize' (Token.DirectChildren(getOffset(t)) :: seqtoken) t
                        | _ -> tokenize' (Token.AllChildren(getOffset(t)) :: acc) t
                | '.' :: t -> 
                    let s, t' = readString "" t
                    tokenize' (Token.CssClass(getOffset(t)+1, s) :: Token.ClassPrefix(getOffset(t)) :: acc) t'
                | '#' :: t -> 
                    let s, t' = readString "" t
                    tokenize' (Token.CssId(getOffset(t)+1, s) :: Token.IdPrefix(getOffset(t)) :: acc) t'
                | '[' :: t ->
                    let s, t' = readString "" t
                    tokenize' (Token.AttributeName(getOffset(t)+1, s) :: Token.OpenAttribute(getOffset(t)) :: acc) t'
                | ']' :: t ->
                    tokenize' (Token.CloseAttribute(getOffset(t)) :: acc) t
                | '=' :: t ->
                    let s, t' = readString "" t
                    tokenize' (Token.AttributeValue(getOffset(t)+1, s) :: Token.Assign(getOffset(t)) :: acc) t'
                | '$' :: '=' :: t ->
                    let s, t' = readString "" t
                    tokenize' (Token.AttributeValue(getOffset(t)+1, s) :: Token.EndWith(getOffset(t)) :: acc) t'
                | '^' :: '=' :: t ->
                    let s, t' = readString "" t
                    tokenize' (Token.AttributeValue(getOffset(t)+1, s) :: Token.StartWith(getOffset(t)) :: acc) t'
                | '|' :: '=' :: t ->
                    let s, t' = readString "" t
                    tokenize' (Token.AttributeValue(getOffset(t)+1, s) :: Token.AttributeContainsPrefix(getOffset(t)) :: acc) t'
                | '*' :: '=' :: t ->
                    let s, t' = readString "" t
                    tokenize' (Token.AttributeValue(getOffset(t)+1, s) :: Token.AttributeContains(getOffset(t)) :: acc) t'

                | '~' :: '=' :: t ->
                    let s, t' = readString "" t
                    tokenize' (Token.AttributeValue(getOffset(t)+1, s) :: Token.AttributeContainsWord(getOffset(t)) :: acc) t'

                | '!' :: '=' :: t ->
                    let s, t' = readString "" t
                    tokenize' (Token.AttributeValue(getOffset(t)+1, s) :: Token.AttributeNotEqual(getOffset(t)) :: acc) t'
 
                |  TokenStr ":checkbox" t  ->
                    let s, t' = readString "" t
                    tokenize' (Token.Checkbox(getOffset(t)+1) :: acc) t'

                |  TokenStr ":selected" t  ->
                    let s, t' = readString "" t
                    tokenize' (Token.Selected(getOffset(t)+1) :: acc) t'

                | TokenStr ":checked" t ->
                    let s, t' = readString "" t
                    tokenize' (Token.Checked(getOffset(t)+1) :: acc) t'

                | TokenStr ":disabled" t ->
                    let s, t' = readString "" t
                    tokenize' (Token.Disabled(getOffset(t)+1) :: acc) t'

                | TokenStr ":enabled" t ->
                    let s, t' = readString "" t
                    tokenize' (Token.Enabled(getOffset(t)+1) :: acc) t'

                | '>' :: t ->
                    let seqtoken = (acc |> List.toSeq |> Seq.skip(1) |> Seq.toList)
                    match acc.Head with
                        | Token.AllChildren(o) -> tokenize' (Token.DirectChildren(getOffset(t)) :: seqtoken) t
                        | _ -> tokenize' (Token.DirectChildren(getOffset(t)) :: acc) t
                | '<' :: t ->
                    let seqtoken = (acc |> List.toSeq |> Seq.skip(1) |> Seq.toList)
                    match acc.Head with
                        | Token.AllChildren(o) -> tokenize' (Token.Ancestor(getOffset(t)) :: seqtoken) t
                        | _ -> tokenize' (Token.Ancestor(getOffset(t)) :: acc) t
                | c :: t when Char.IsLetterOrDigit(c) -> 
                    let str = c.ToString()
                    let s, t' = readString str t
                    tokenize' (Token.TagName(getOffset(t), s) :: acc) t'
                | [] -> List.rev acc // A la fin, on inverse la liste, car la call stack nous sort les tokens à l'envers
                | c :: t when Char.IsLetterOrDigit(c) <> true ->
                    let offset = getOffset t
                    failwith (sprintf "Invalid css selector syntax (char '%c' at offset %d)" c offset)
                | _ ->
                    failwith "Invalid css selector syntax"
            tokenize' [] source
