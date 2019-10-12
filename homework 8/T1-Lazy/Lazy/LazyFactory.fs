namespace Lazy

/// Фабрика, обеспечивающая создание ленивых объектов в однопоточном и многопоточном режиме
module LazyFactory = 

    /// Создает ленивый объект на основе вычисления с гарантией корректной работы в однопоточном режиме
    let createOneThreadLazy supplier = 
        OneThreadLazy(supplier) :> ILazy<'a>
    
    /// Создает ленивый объект на основе вычисления с гарантией корректной работы в многопоточном режиме
    let createMultiThreadLazy supplier = 
        MultiThreadLazy(supplier) :> ILazy<'a>

    /// Создает ленивый объект на основе вычисления с гарантией корректной работы в многопоточном режиме,
    /// не блокирует поток при выполнении
    let createLockFreeLazy supplier = 
        LockFreeLazy(supplier) :> ILazy<'a>