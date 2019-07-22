namespace Task2

module MaxPalindrome =
    /// Check if string is palindrome
    let isPalindrome (str: string) = 
        let charList = str |> Seq.toList    
        charList |> List.rev = charList

    /// Check if number is palindrome
    let isPalindromeNum (x: int) = 
        isPalindrome <| x.ToString ()

    /// List of three digit numbers
    let threeDigitNumbers = [100 .. 999]

    /// Func, returns max three digit palindrome
    let getMaxThreeDigitPalindrome () = 
        threeDigitNumbers
        |> List.collect (fun x -> List.map ((*) x) threeDigitNumbers) 
        |> List.filter isPalindromeNum
        |> List.max