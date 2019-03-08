module Palindrome

/// Check if string is palindrome
let isPalindrome (str: string) = 
    let charList = str |> Seq.toList    
    charList |> List.rev = charList
