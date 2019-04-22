Welcom!!!!!!
\page "Lua简介"
# 1 – 简介
Lua 是一门扩展式程序设计语言，被设计成支持通用过程式编程，并有相关数据描述设施。 同时对面向对象编程、函数式编程和数据驱动式编程也提供了良好的支持。 它作为一个强大、轻量的嵌入式脚本语言，可供任何需要的程序使用。 Lua 由 clean C（标准 C 和 C++ 间共通的子集） 实现成一个库。

作为一门扩展式语言，Lua 没有 "main" 程序的概念： 它只能 嵌入 一个宿主程序中工作， 该宿主程序被称为 被嵌入程序 或者简称 宿主 。 宿主程序可以调用函数执行一小段 Lua 代码，可以读写 Lua 变量，可以注册 C 函数让 Lua 代码调用。 依靠 C 函数，Lua 可以共享相同的语法框架来定制编程语言，从而适用不同的领域。 Lua 的官方发布版包含一个叫做 lua 的宿主程序示例， 它是一个利用 Lua 库实现的完整独立的 Lua 解释器，可用于交互式应用或批处理。

Lua 是一个自由软件，其使用许可证决定了它的使用过程无需任何担保。 本手册所描述的实现可以在 Lua 的官方网站 www.lua.org 找到。

与其它的许多参考手册一样，这份文档有些地方比较枯燥。 关于 Lua 背后的设计思想， 可以看看 Lua 网站上提供的技术论文。 至于用 Lua 编程的细节介绍， 请参阅 Roberto 的书，Programming in Lua。

# 2 – 基本概念
本章描述了语言的基本概念。

## 2.1 – 值与类型
Lua 是一门动态类型语言。 这意味着变量没有类型；只有值才有类型。 语言中不设类型定义。 所有的值携带自己的类型。

Lua 中所有的值都是 一等公民。 这意味着所有的值均可保存在变量中、 当作参数传递给其它函数、以及作为返回值。

Lua 中有八种基本类型： nil、boolean、number、string、function、userdata、 thread 和 table。 Nil 是值 nil 的类型， 其主要特征就是和其它值区别开；通常用来表示一个有意义的值不存在时的状态。 Boolean 是 false 与 true 两个值的类型。 nil 和 false 都会导致条件判断为假； 而其它任何值都表示为真。 Number 代表了整数和实数（浮点数）。 String 表示一个不可变的字节序列。 Lua 对 8 位是友好的： 字符串可以容纳任意 8 位值， 其中包含零 ('\0') 。 Lua 的字符串与编码无关； 它不关心字符串中具体内容。

number 类型有两种内部表现方式， 整数 和 浮点数。 对于何时使用哪种内部形式，Lua 有明确的规则， 但它也按需（参见 §3.4.3）作自动转换。 因此，程序员多数情况下可以选择忽略整数与浮点数之间的差异或者假设完全控制每个数字的内部表现方式。 标准 Lua 使用 64 位整数和双精度（64 位）浮点数， 但你也可以把 Lua 编译成使用 32 位整数和单精度（32 位）浮点数。 以 32 位表示数字对小型机器以及嵌入式系统特别合适。 （参见 luaconf.h 文件中的宏 LUA_32BITS 。）

Lua 可以调用（以及操作）用 Lua 或 C （参见 §3.4.10）编写的函数。 这两种函数有统一类型 function。

userdata 类型允许将 C 中的数据保存在 Lua 变量中。 用户数据类型的值是一个内存块， 有两种用户数据： 完全用户数据 ，指一块由 Lua 管理的内存对应的对象； 轻量用户数据 ，则指一个简单的 C 指针。 用户数据在 Lua 中除了赋值与相等性判断之外没有其他预定义的操作。 通过使用 元表 ，程序员可以给完全用户数据定义一系列的操作 （参见 §2.4）。 你只能通过 C API 而无法在 Lua 代码中创建或者修改用户数据的值， 这保证了数据仅被宿主程序所控制。

thread 类型表示了一个独立的执行序列，被用于实现协程 （参见 §2.6）。 Lua 的线程与操作系统的线程毫无关系。 Lua 为所有的系统，包括那些不支持原生线程的系统，提供了协程支持。

table 是一个关联数组， 也就是说，这个数组不仅仅以数字做索引，除了 nil 和 NaN 之外的所有 Lua 值 都可以做索引。 （Not a Number 是一个特殊的数字，它用于表示未定义或表示不了的运算结果，比如 0/0。） 表可以是 异构 的； 也就是说，表内可以包含任何类型的值（ nil 除外）。 任何键的值若为 nil 就不会被记入表结构内部。 换言之，对于表内不存在的键，都对应着值 nil 。

表是 Lua 中唯一的数据结构， 它可被用于表示普通数组、序列、符号表、集合、记录、图、树等等。 对于记录，Lua 使用域名作为索引。 语言提供了 a.name 这样的语法糖来替代 a["name"] 这种写法以方便记录这种结构的使用。 在 Lua 中有多种便利的方式创建表（参见 §3.4.9）。

我们使用 序列 这个术语来表示一个用 {1..n} 的正整数集做索引的表。 这里的非负整数 n 被称为该序列的长度（参见 §3.4.7）。

和索引一样，表中每个域的值也可以是任何类型。 需要特别指出的是：既然函数是一等公民，那么表的域也可以是函数。 这样，表就可以携带 方法 了。 （参见 §3.4.11）。

索引一张表的原则遵循语言中的直接比较规则。 当且仅当 i 与 j直接比较相等时 （即不通过元方法的比较）， 表达式 a[i] 与 a[j] 表示了表中相同的元素。 特别指出：一个可以完全表示为整数的浮点数和对应的整数相等 （例如：1.0 == 1）。 为了消除歧义，当一个可以完全表示为整数的浮点数做为键值时， 都会被转换为对应的整数储存。 例如，当你写 a[2.0] = true 时， 实际被插入表中的键是整数 2 。 （另一方面，2 与 "2" 是两个不同的 Lua 值， 故而它们可以是同一张表中的不同项。）

表、函数、线程、以及完全用户数据在 Lua 中被称为 对象： 变量并不真的 持有 它们的值，而仅保存了对这些对象的 引用。 赋值、参数传递、函数返回，都是针对引用而不是针对值的操作， 这些操作均不会做任何形式的隐式拷贝。

库函数 type 用于以字符串形式返回给定值的类型。 （参见 §6.1）。

## 2.2 – 环境与全局环境
后面在 §3.2 以及 §3.3.3 会讨论， 引用一个叫 var 的自由名字（指在任何层级都未被声明的名字） 在句法上都被翻译为 _ENV.var 。 此外，每个被编译的 Lua 代码块都会有一个外部的局部变量叫 _ENV （参见 §3.3.2）， 因此，_ENV 这个名字永远都不会成为一个代码块中的自由名字。

在转译那些自由名字时，_ENV 是否是那个外部的局部变量无所谓。 _ENV 和其它你可以使用的变量名没有区别。 这里特别指出，你可以定义一个新变量或指定一个参数叫这个名字。 当编译器在转译自由名字时所用到的 _ENV ， 指的是你的程序在那个点上可见的那个名为 _ENV 的变量。 （Lua 的可见性规则参见 §3.5）

被 _ENV 用于值的那张表被称为 环境。

Lua 保有一个被称为 全局环境 特别环境。它被保存在 C 注册表 （参见 §4.5）的一个特别索引下。 在 Lua 中，全局变量 _G 被初始化为这个值。 （_G 不被内部任何地方使用。）

当 Lua 加载一个代码块，_ENV 这个上值的默认值就是这个全局环境 （参见 load）。 因此，在默认情况下，Lua 代码中提及的自由名字都指的全局环境中的相关项 （因此，它们也被称为 全局变量 ）。 此外，所有的标准库都被加载入全局环境，一些函数也针对这个环境做操作。 你可以用 load （或 loadfile）加载代码块，并赋予它们不同的环境。 （在 C 里，当你加载一个代码块后，可以通过改变它的第一个上值来改变它的环境。）

## 2.3 – 错误处理
由于 Lua 是一门嵌入式扩展语言，其所有行为均源于宿主程序中 C 代码对某个 Lua 库函数的调用。 （单独使用 Lua 时，lua 程序就是宿主程序。） 所以，在编译或运行 Lua 代码块的过程中，无论何时发生错误， 控制权都返回给宿主，由宿主负责采取恰当的措施（比如打印错误消息）。

可以在 Lua 代码中调用 error 函数来显式地抛出一个错误。 如果你需要在 Lua 中捕获这些错误， 可以使用 pcall 或 xpcall 在 保护模式 下调用一个函数。

无论何时出现错误，都会抛出一个携带错误信息的 错误对象 （错误消息）。 Lua 本身只会为错误生成字符串类型的错误对象， 但你的程序可以为错误生成任何类型的错误对象， 这就看你的 Lua 程序或宿主程序如何处理这些错误对象。

使用 xpcall 或 lua_pcall 时， 你应该提供一个 消息处理函数 用于错误抛出时调用。 该函数需接收原始的错误消息，并返回一个新的错误消息。 它在错误发生后栈尚未展开时调用， 因此可以利用栈来收集更多的信息， 比如通过探知栈来创建一组栈回溯信息。 同时，该处理函数也处于保护模式下，所以该函数内发生的错误会再次触发它（递归）。 如果递归太深，Lua 会终止调用并返回一个合适的消息。

## 2.4 – 元表及元方法
Lua 中的每个值都可以有一个 元表。 这个 元表 就是一个普通的 Lua 表， 它用于定义原始值在特定操作下的行为。 如果你想改变一个值在特定操作下的行为，你可以在它的元表中设置对应域。 例如，当你对非数字值做加操作时， Lua 会检查该值的元表中的 "__add" 域下的函数。 如果能找到，Lua 则调用这个函数来完成加这个操作。

元表中的键对应着不同的 事件 名； 键关联的那些值被称为 元方法。 在上面那个例子中引用的事件为 "add" ， 完成加操作的那个函数就是元方法。

你可以用 getmetatable 函数 来获取任何值的元表。

使用 setmetatable 来替换一张表的元表。在 Lua 中，你不可以改变表以外其它类型的值的元表 （除非你使用调试库（参见§6.10））； 若想改变这些非表类型的值的元表，请使用 C API。

表和完全用户数据有独立的元表 （当然，多个表和用户数据可以共享同一个元表）。 其它类型的值按类型共享元表； 也就是说所有的数字都共享同一个元表， 所有的字符串共享另一个元表等等。 默认情况下，值是没有元表的， 但字符串库在初始化的时候为字符串类型设置了元表 （参见 §6.4）。

元表决定了一个对象在数学运算、位运算、比较、连接、 取长度、调用、索引时的行为。 元表还可以定义一个函数，当表对象或用户数据对象在垃圾回收 （参见§2.5）时调用它。

接下来会给出一张元表可以控制的事件的完整列表。 每个操作都用对应的事件名来区分。 每个事件的键名用加有 '__' 前缀的字符串来表示； 例如 "add" 操作的键名为字符串 "__add"。 注意、Lua 从元表中直接获取元方法； 访问元表中的元方法永远不会触发另一次元方法。 下面的代码模拟了 Lua 从一个对象 obj 中获取一个元方法的过程：

     rawget(getmetatable(obj) or {}, "__" .. event_name)
对于一元操作符（取负、求长度、位反）， 元方法调用的时候，第二个参数是个哑元，其值等于第一个参数。 这样处理仅仅是为了简化 Lua 的内部实现 （这样处理可以让所有的操作都和二元操作一致）， 这个行为有可能在将来的版本中移除。 （使用这个额外参数的行为都是不确定的。）

"add": + 操作。 如果任何不是数字的值（包括不能转换为数字的字符串）做加法， Lua 就会尝试调用元方法。 首先、Lua 检查第一个操作数（即使它是合法的）， 如果这个操作数没有为 "__add" 事件定义元方法， Lua 就会接着检查第二个操作数。 一旦 Lua 找到了元方法， 它将把两个操作数作为参数传入元方法， 元方法的结果（调整为单个值）作为这个操作的结果。 如果找不到元方法，将抛出一个错误。
"sub": - 操作。 行为和 "add" 操作类似。
"mul": * 操作。 行为和 "add" 操作类似。
"div": / 操作。 行为和 "add" 操作类似。
"mod": % 操作。 行为和 "add" 操作类似。
"pow": ^ （次方）操作。 行为和 "add" 操作类似。
"unm": - （取负）操作。 行为和 "add" 操作类似。
"idiv": // （向下取整除法）操作。 行为和 "add" 操作类似。
"band": & （按位与）操作。 行为和 "add" 操作类似， 不同的是 Lua 会在任何一个操作数无法转换为整数时 （参见 §3.4.3）尝试取元方法。
"bor": | （按位或）操作。 行为和 "band" 操作类似。
"bxor": ~ （按位异或）操作。 行为和 "band" 操作类似。
"bnot": ~ （按位非）操作。 行为和 "band" 操作类似。
"shl": << （左移）操作。 行为和 "band" 操作类似。
"shr": >> （右移）操作。 行为和 "band" 操作类似。
"concat": .. （连接）操作。 行为和 "add" 操作类似， 不同的是 Lua 在任何操作数即不是一个字符串 也不是数字（数字总能转换为对应的字符串）的情况下尝试元方法。
"len": # （取长度）操作。 如果对象不是字符串，Lua 会尝试它的元方法。 如果有元方法，则调用它并将对象以参数形式传入， 而返回值（被调整为单个）则作为结果。 如果对象是一张表且没有元方法， Lua 使用表的取长度操作（参见 §3.4.7）。 其它情况，均抛出错误。
"eq": == （等于）操作。 和 "add" 操作行为类似， 不同的是 Lua 仅在两个值都是表或都是完全用户数据 且它们不是同一个对象时才尝试元方法。 调用的结果总会被转换为布尔量。
"lt": < （小于）操作。 和 "add" 操作行为类似， 不同的是 Lua 仅在两个值不全为整数也不全为字符串时才尝试元方法。 调用的结果总会被转换为布尔量。
"le": <= （小于等于）操作。 和其它操作不同， 小于等于操作可能用到两个不同的事件。 首先，像 "lt" 操作的行为那样，Lua 在两个操作数中查找 "__le" 元方法。 如果一个元方法都找不到，就会再次查找 "__lt" 事件， 它会假设 a <= b 等价于 not (b < a)。 而其它比较操作符类似，其结果会被转换为布尔量。
"index": 索引 table[key]。 当 table 不是表或是表 table 中不存在 key 这个键时，这个事件被触发。 此时，会读出 table 相应的元方法。
尽管名字取成这样， 这个事件的元方法其实可以是一个函数也可以是一张表。 如果它是一个函数，则以 table 和 key 作为参数调用它。 如果它是一张表，最终的结果就是以 key 取索引这张表的结果。 （这个索引过程是走常规的流程，而不是直接索引， 所以这次索引有可能引发另一次元方法。）

"newindex": 索引赋值 table[key] = value 。 和索引事件类似，它发生在 table 不是表或是表 table 中不存在 key 这个键的时候。 此时，会读出 table 相应的元方法。
同索引过程那样， 这个事件的元方法即可以是函数，也可以是一张表。 如果是一个函数， 则以 table、 key、以及 value 为参数传入。 如果是一张表， Lua 对这张表做索引赋值操作。 （这个索引过程是走常规的流程，而不是直接索引赋值， 所以这次索引赋值有可能引发另一次元方法。）

一旦有了 "newindex" 元方法， Lua 就不再做最初的赋值操作。 （如果有必要，在元方法内部可以调用 rawset 来做赋值。）

"call": 函数调用操作 func(args)。 当 Lua 尝试调用一个非函数的值的时候会触发这个事件 （即 func 不是一个函数）。 查找 func 的元方法， 如果找得到，就调用这个元方法， func 作为第一个参数传入，原来调用的参数（args）后依次排在后面。
2.5 – 垃圾收集
Lua 采用了自动内存管理。 这意味着你不用操心新创建的对象需要的内存如何分配出来， 也不用考虑在对象不再被使用后怎样释放它们所占用的内存。 Lua 运行了一个 垃圾收集器 来收集所有 死对象 （即在 Lua 中不可能再访问到的对象）来完成自动内存管理的工作。 Lua 中所有用到的内存，如：字符串、表、用户数据、函数、线程、 内部结构等，都服从自动管理。

Lua 实现了一个增量标记-扫描收集器。 它使用这两个数字来控制垃圾收集循环： 垃圾收集器间歇率 和 垃圾收集器步进倍率。 这两个数字都使用百分数为单位 （例如：值 100 在内部表示 1 ）。

垃圾收集器间歇率控制着收集器需要在开启新的循环前要等待多久。 增大这个值会减少收集器的积极性。 当这个值比 100 小的时候，收集器在开启新的循环前不会有等待。 设置这个值为 200 就会让收集器等到总内存使用量达到 之前的两倍时才开始新的循环。

垃圾收集器步进倍率控制着收集器运作速度相对于内存分配速度的倍率。 增大这个值不仅会让收集器更加积极，还会增加每个增量步骤的长度。 不要把这个值设得小于 100 ， 那样的话收集器就工作的太慢了以至于永远都干不完一个循环。 默认值是 200 ，这表示收集器以内存分配的“两倍”速工作。

如果你把步进倍率设为一个非常大的数字 （比你的程序可能用到的字节数还大 10% ）， 收集器的行为就像一个 stop-the-world 收集器。 接着你若把间歇率设为 200 ， 收集器的行为就和过去的 Lua 版本一样了： 每次 Lua 使用的内存翻倍时，就做一次完整的收集。

你可以通过在 C 中调用 lua_gc 或在 Lua 中调用 collectgarbage 来改变这俩数字。 这两个函数也可以用来直接控制收集器（例如停止它或重启它）。

### 2.5.1 – 垃圾收集元方法
你可以为表设定垃圾收集的元方法， 对于完全用户数据（参见 §2.4）， 则需要使用 C API 。 该元方法被称为 终结器。 终结器允许你配合 Lua 的垃圾收集器做一些额外的资源管理工作 （例如关闭文件、网络或数据库连接，或是释放一些你自己的内存）。

如果要让一个对象（表或用户数据）在收集过程中进入终结流程， 你必须 标记 它需要触发终结器。 当你为一个对象设置元表时，若此刻这张元表中用一个以字符串 "__gc" 为索引的域，那么就标记了这个对象需要触发终结器。 注意：如果你给对象设置了一个没有 __gc 域的元表，之后才给元表加上这个域， 那么这个对象是没有被标记成需要触发终结器的。 然而，一旦对象被标记， 你还是可以自由的改变其元表中的 __gc 域的。

当一个被标记的对象成为了垃圾后， 垃圾收集器并不会立刻回收它。 取而代之的是，Lua 会将其置入一个链表。 在收集完成后，Lua 将遍历这个链表。 Lua 会检查每个链表中的对象的 __gc 元方法：如果是一个函数，那么就以对象为唯一参数调用它； 否则直接忽略它。

在每次垃圾收集循环的最后阶段， 本次循环中检测到的需要被回收之对象， 其终结器的触发次序按当初给对象作需要触发终结器的标记之次序的逆序进行； 这就是说，第一个被调用的终结器是程序中最后一个被标记的对象所携的那个。 每个终结器的运行可能发生在执行常规代码过程中的任意一刻。

由于被回收的对象还需要被终结器使用， 该对象（以及仅能通过它访问到的其它对象）一定会被 Lua 复活。 通常，复活是短暂的，对象所属内存会在下一个垃圾收集循环释放。 然后，若终结器又将对象保存去一些全局的地方 （例如：放在一个全局变量里），这次复活就持续生效了。 此外，如果在终结器中对一个正进入终结流程的对象再次做一次标记让它触发终结器， 只要这个对象在下个循环中依旧不可达，它的终结函数还会再调用一次。 无论是哪种情况， 对象所属内存仅在垃圾收集循环中该对象不可达且 没有被标记成需要触发终结器才会被释放。

当你关闭一个状态机（参见 lua_close）， Lua 将调用所有被标记了需要触发终结器对象的终结过程， 其次序为标记次序的逆序。 在这个过程中，任何终结器再次标记对象的行为都不会生效。

### 2.5.2 – 弱表
弱表 指内部元素为 弱引用 的表。 垃圾收集器会忽略掉弱引用。 换句话说，如果一个对象只被弱引用引用到， 垃圾收集器就会回收这个对象。

一张弱表可以有弱键或是弱值，也可以键值都是弱引用。 仅含有弱键的表允许收集器回收它的键，但会阻止对值所指的对象被回收。 若一张表的键值均为弱引用， 那么收集器可以回收其中的任意键和值。 任何情况下，只要键或值的任意一项被回收， 相关联的键值对都会从表中移除。 一张表的元表中的 __mode 域控制着这张表的弱属性。 当 __mode 域是一个包含字符 'k' 的字符串时，这张表的所有键皆为弱引用。 当 __mode 域是一个包含字符 'v' 的字符串时，这张表的所有值皆为弱引用。

属性为弱键强值的表也被称为 暂时表。 对于一张暂时表， 它的值是否可达仅取决于其对应键是否可达。 特别注意，如果表内的一个键仅仅被其值所关联引用， 这个键值对将被表内移除。

对一张表的弱属性的修改仅在下次收集循环才生效。 尤其是当你把表由弱改强，Lua 还是有可能在修改生效前回收表内一些项目。

只有那些有显式构造过程的对象才会从弱表中移除。 值，例如数字和轻量 C 函数，不受垃圾收集器管辖， 因此不会从弱表中移除 （除非它们的关联项被回收）。 虽然字符串受垃圾回收器管辖， 但它们没有显式的构造过程，所以也不会从弱表中移除。

弱表针对复活的对象 （指那些正在走终结流程，仅能被终结器访问的对象） 有着特殊的行为。 弱值引用的对象，在运行它们的终结器前就被移除了， 而弱键引用的对象则要等到终结器运行完毕后，到下次收集当对象真的被释放时才被移除。 这个行为使得终结器运行时得以访问到由该对象在弱表中所关联的属性。

如果一张弱表在当次收集循环内的复活对象中， 那么在下个循环前这张表有可能未被正确地清理。

## 2.6 – 协程
Lua 支持协程，也叫 协同式多线程。 一个协程在 Lua 中代表了一段独立的执行线程。 然而，与多线程系统中的线程的区别在于， 协程仅在显式调用一个让出（yield）函数时才挂起当前的执行。

调用函数 coroutine.create 可创建一个协程。 其唯一的参数是该协程的主函数。 create 函数只负责新建一个协程并返回其句柄 （一个 thread 类型的对象）； 而不会启动该协程。

调用 coroutine.resume 函数执行一个协程。 第一次调用 coroutine.resume 时，第一个参数应传入 coroutine.create 返回的线程对象，然后协程从其主函数的第一行开始执行。 传递给 coroutine.resume 的其他参数将作为协程主函数的参数传入。 协程启动之后，将一直运行到它终止或 让出。

协程的运行可能被两种方式终止： 正常途径是主函数返回 （显式返回或运行完最后一条指令）； 非正常途径是发生了一个未被捕获的错误。 对于正常结束， coroutine.resume 将返回 true， 并接上协程主函数的返回值。 当错误发生时， coroutine.resume 将返回 false 与错误消息。

通过调用 coroutine.yield 使协程暂停执行，让出执行权。 协程让出时，对应的最近 coroutine.resume 函数会立刻返回，即使该让出操作发生在内嵌函数调用中 （即不在主函数，但在主函数直接或间接调用的函数内部）。 在协程让出的情况下， coroutine.resume 也会返回 true， 并加上传给 coroutine.yield 的参数。 当下次重启同一个协程时， 协程会接着从让出点继续执行。 此时，此前让出点处对 coroutine.yield 的调用 会返回，返回值为传给 coroutine.resume 的第一个参数之外的其他参数。

与 coroutine.create 类似， coroutine.wrap 函数也会创建一个协程。 不同之处在于，它不返回协程本身，而是返回一个函数。 调用这个函数将启动该协程。 传递给该函数的任何参数均当作 coroutine.resume 的额外参数。 coroutine.wrap 返回 coroutine.resume 的所有返回值，除了第一个返回值（布尔型的错误码）。 和 coroutine.resume 不同， coroutine.wrap 不会捕获错误； 而是将任何错误都传播给调用者。

下面的代码展示了一个协程工作的范例：

     function foo (a)
       print("foo", a)
       return coroutine.yield(2*a)
     end
     
     co = coroutine.create(function (a,b)
           print("co-body", a, b)
           local r = foo(a+1)
           print("co-body", r)
           local r, s = coroutine.yield(a+b, a-b)
           print("co-body", r, s)
           return b, "end"
     end)
     
     print("main", coroutine.resume(co, 1, 10))
     print("main", coroutine.resume(co, "r"))
     print("main", coroutine.resume(co, "x", "y"))
     print("main", coroutine.resume(co, "x", "y"))
当你运行它，将产生下列输出：

     co-body 1       10
     foo     2
     main    true    4
     co-body r
     main    true    11      -9
     co-body x       y
     main    true    10      end
     main    false   cannot resume dead coroutine
你也可以通过 C API 来创建及操作协程： 参见函数 lua_newthread， lua_resume， 以及 lua_yield。

# 3 – 语言定义
这一章描述了 Lua 的词法、语法和句法。 换句话说，本章描述哪些符记是有效的， 它们如何被组合起来，这些组合方式有什么含义。

关于语言的构成概念将用常见的扩展 BNF 表达式写出。 也就是这个样子： {a} 表示 0 或多个 a， [a] 表示一个可选的 a。 可以被分解的非最终符号会这样写 non-terminal ， 关键字会写成这样 kword， 而其它不能被分解的最终符号则写成这样 ‘=’ 。 完整的 Lua 语法可以在本手册最后一章 §9 找到。

## 3.1 – 词法约定
Lua 语言的格式自由。 它会忽略语法元素（符记）间的空格（包括换行）和注释， 仅把它们看作为名字和关键字间的分割符。

Lua 中的 名字 （也被称为 标识符） 可以是由非数字打头的任意字母下划线和数字构成的字符串。 标识符可用于对变量、表的域、以及标签命名。

下列 关键字 是保留的，不可用于名字：

     and       break     do        else      elseif    end
     false     for       function  goto      if        in
     local     nil       not       or        repeat    return
     then      true      until     while
Lua 语言对大小写敏感： and 是一个保留字，但 And 与 AND 则是两个不同的有效名字。 作为一个约定，程序应避免创建以下划线加一个或多个大写字母构成的名字 （例如 _VERSION）。

下列字符串是另外一些符记：

     +     -     *     /     %     ^     #
     &     ~     |     <<    >>    //
     ==    ~=    <=    >=    <     >     =
     (     )     {     }     [     ]     ::
     ;     :     ,     .     ..    ...
字面串 可以用单引号或双引号括起。 字面串内部可以包含下列 C 风格的转义串： '\a' （响铃）， '\b' （退格）， '\f' （换页）， '\n' （换行）， '\r' （回车）， '\t' （横项制表）， '\v' （纵向制表）， '\\' （反斜杠）， '\"' （双引号）， 以及 '\'' (单引号)。 在反斜杠后跟一个真正的换行等价于在字符串中写一个换行符。 转义串 '\z' 会忽略其后的一系列空白符，包括换行； 它在你需要对一个很长的字符串常量断行为多行并希望在每个新行保持缩进时非常有用。

Lua 中的字符串可以保存任意 8 位值，其中包括用 '\0' 表示的 0 。 一般而言，你可以用字符的数字值来表示这个字符。 方式是用转义串 \xXX， 此处的 XX 必须是恰好两个字符的 16 进制数。 或者你也可以使用转义串 \ddd ， 这里的 ddd 是一到三个十进制数字。 （注意，如果在转义符后接着恰巧是一个数字符号的话， 你就必须在这个转义形式中写满三个数字。）

对于用 UTF-8 编码的 Unicode 字符，你可以用 转义符 \u{XXX} 来表示 （这里必须有一对花括号）， 此处的 XXX 是用 16 进制表示的字符编号。

字面串还可以用一种 长括号 括起来的方式定义。 我们把两个正的方括号间插入 n 个等号定义为 第 n 级开长括号。 就是说，0 级开的长括号写作 [[ ， 一级开长括号写作 [=[ ， 如此等等。 闭长括号也作类似定义； 举个例子，4 级反的长括号写作 ]====] 。 一个 长字面串 可以由任何一级的开长括号开始，而由第一个碰到的同级的闭长括号结束。 这种方式描述的字符串可以包含任何东西，当然特定级别的反长括号除外。 整个词法分析过程将不受分行限制，不处理任何转义符，并且忽略掉任何不同级别的长括号。 其中碰到的任何形式的换行串（回车、换行、回车加换行、换行加回车），都会被转换为单个换行符。

字面串中的每个不被上述规则影响的字节都呈现为本身。 然而，Lua 是用文本模式打开源文件解析的， 一些系统的文件操作函数对某些控制字符的处理可能有问题。 因此，对于非文本数据，用引号括起来并显式按转义符规则来表述更安全。

为了方便起见， 当一个开长括号后紧接一个换行符时， 这个换行符不会放在字符串内。 举个例子，假设一个系统使用 ASCII 码 （此时 'a' 编码为 97 ， 换行编码为 10 ，'1' 编码为 49 ）， 下面五种方式描述了完全相同的字符串：

     a = 'alo\n123"'
     a = "alo\n123\""
     a = '\97lo\10\04923"'
     a = [[alo
     123"]]
     a = [==[
     alo
     123"]==]
数字常量 （或称为 数字量） 可以由可选的小数部分和可选的十为底的指数部分构成， 指数部分用字符 'e' 或 'E' 来标记。 Lua 也接受以 0x 或 0X 开头的 16 进制常量。 16 进制常量也接受小数加指数部分的形式，指数部分是以二为底， 用字符 'p' 或 'P' 来标记。 数字常量中包含小数点或指数部分时，被认为是一个浮点数； 否则被认为是一个整数。 下面有一些合法的整数常量的例子：

     3   345   0xff   0xBEBADA
以下为合法的浮点常量：

     3.0     3.1416     314.16e-2     0.31416E1     34e1
     0x0.1E  0xA23p-4   0X1.921FB54442D18P+1
在字符串外的任何地方出现以双横线 (--) 开头的部分是 注释 。 如果 -- 后没有紧跟着一个开大括号， 该注释为 短注释， 注释到当前行末截至。 否则，这是一段 长注释 ， 注释区一直维持到对应的闭长括号。 长注释通常用于临时屏蔽掉一大段代码。

## 3.2 – 变量
变量是储存值的地方。 Lua 中有三种变量： 全局变量、局部变量和表的域。

单个名字可以指代一个全局变量也可以指代一个局部变量 （或者是一个函数的形参，这是一种特殊形式的局部变量）。

	var ::= Name
名字指 §3.1 中定义的标识符。

所有没有显式声明为局部变量（参见 §3.3.7） 的变量名都被当做全局变量。 局部变量有其 作用范围 ： 局部变量可以被定义在它作用范围中的函数自由使用（参见 §3.5）。

在变量的首次赋值之前，变量的值均为 nil。

方括号被用来对表作索引：

	var ::= prefixexp ‘[’ exp ‘]’
对全局变量以及表的域之访问的含义可以通过元表来改变。 以索引方式访问一个变量 t[i] 等价于 调用 gettable_event(t,i)。 （参见 §2.4 ，有一份完整的关于 gettable_event 函数的说明。 这个函数并没有在 lua 中定义出来，也不能在 lua 中调用。这里我们把提到它只是方便说明问题。）

var.Name 这种语法只是一个语法糖，用来表示 var["Name"]：

	var ::= prefixexp ‘.’ Name
对全局变量 x 的操作等价于操作 _ENV.x。 由于代码块编译的方式， _ENV 永远也不可能是一个全局名字 （参见 §2.2）。

## 3.3 – 语句
Lua 支持所有与 Pascal 或是 C 类似的常见形式的语句， 这个集合包括赋值，控制结构，函数调用，还有变量声明。

### 3.3.1 – 语句块
语句块是一个语句序列，它们会按次序执行：

	block ::= {stat}
Lua 支持 空语句， 你可以用分号分割语句，也可以以分号开始一个语句块， 或是连着写两个分号：

	stat ::= ‘;’
函数调用和赋值语句都可能以一个小括号打头， 这可能让 Lua 的语法产生歧义。 我们来看看下面的代码片断：

     a = b + c
     (print or io.write)('done')
从语法上说，可能有两种解释方式：

     a = b + c(print or io.write)('done')
     
     a = b + c; (print or io.write)('done')
当前的解析器总是用第一种结构来解析， 它会将开括号看成函数调用的参数传递开始处。 为了避免这种二义性， 在一条语句以小括号开头时，前面放一个分号是个好习惯：

     ;(print or io.write)('done')
一个语句块可以被显式的定界为单条语句：

	stat ::= do block end
显式的对一个块定界通常用来控制内部变量声明的作用域。 有时，显式定界也用于在一个语句块中间插入 return （参见 §3.3.4）。

### 3.3.2 – 代码块
Lua 的一个编译单元被称为一个 代码块。 从句法构成上讲，一个代码块就是一个语句块。

	chunk ::= block
Lua 把一个代码块当作一个拥有不定参数的匿名函数 （参见§3.4.11）来处理。 正是这样，代码块内可以定义局部变量，它可以接收参数，返回若干值。 此外，这个匿名函数在编译时还为它的作用域绑定了一个外部局部变量 _ENV （参见 §2.2）。 该函数总是把 _ENV 作为它唯一的一个上值， 即使这个函数不使用这个变量，它也存在。

代码块可以被保存在文件中，也可以作为宿主程序内部的一个字符串。 要执行一个代码块， 首先要让 Lua 加载 它， 将代码块中的代码预编译成虚拟机中的指令， 而后，Lua 用虚拟机解释器来运行编译后的代码。

代码块可以被预编译为二进制形式； 参见程序 luac 以及函数 string.dump 可获得更多细节。 用源码表示的程序和编译后的形式可自由替换； Lua 会自动检测文件格式做相应的处理 （参见 load）。

### 3.3.3 – 赋值
Lua 允许多重赋值。 因此，赋值的语法定义是等号左边放一个变量列表， 而等号右边放一个表达式列表。 两边的列表中的元素都用逗号间开：

	stat ::= varlist ‘=’ explist
	varlist ::= var {‘,’ var}
	explist ::= exp {‘,’ exp}
表达式放在 §3.4 中讨论。

在作赋值操作之前， 那值列表会被 调整 为左边变量列表的个数。 如果值比需要的更多的话，多余的值就被扔掉。 如果值的数量不够需求， 将会按所需扩展若干个 nil。 如果表达式列表以一个函数调用结束， 这个函数所返回的所有值都会在调整操作之前被置入值列表中 （除非这个函数调用被用括号括了起来；参见 §3.4）。

赋值语句首先让所有的表达式完成运算， 之后再做赋值操作。 因此，下面这段代码

     i = 3
     i, a[i] = i+1, 20
会把 a[3] 设置为 20，而不会影响到 a[4] 。 这是因为 a[i] 中的 i 在被赋值为 4 之前就被计算出来了（当时是 3 ）。 简单说 ，这样一行

     x, y = y, x
会交换 x 和 y 的值， 及

     x, y, z = y, z, x
会轮换 x，y，z 的值。

对全局变量以及表的域的赋值操作的含义可以通过元表来改变。 对 t[i] = val 这样的变量索引赋值， 等价于 settable_event(t,i,val)。 （关于函数 settable_event 的详细说明，参见 §2.4。 这个函数并没有在 Lua 中定义出来，也不可以被调用。 这里我们列出来，仅仅出于方便解释的目的。）

对于全局变量 x = val 的赋值等价于 _ENV.x = val （参见 §2.2）。

### 3.3.4 – 控制结构
if, while, and repeat 这些控制结构符合通常的意义，而且也有类似的语法：

	stat ::= while exp do block end
	stat ::= repeat block until exp
	stat ::= if exp then block {elseif exp then block} [else block] end
Lua 也有一个 for 语句，它有两种形式 （参见 §3.3.5）。

控制结构中的条件表达式可以返回任何值。 false 与 nil 两者都被认为是假。 所有不同于 nil 与 false 的其它值都被认为是真 （特别需要注意的是，数字 0 和空字符串也被认为是真）。

在 repeat–until 循环中， 内部语句块的结束点不是在 until 这个关键字处， 它还包括了其后的条件表达式。 因此，条件表达式中可以使用循环内部语句块中的定义的局部变量。

goto 语句将程序的控制点转移到一个标签处。 由于句法上的原因， Lua 里的标签也被认为是语句：

	stat ::= goto Name
	stat ::= label
	label ::= ‘::’ Name ‘::’
除了在内嵌函数中，以及在内嵌语句块中定义了同名标签，的情况外， 标签对于它定义所在的整个语句块可见。 只要 goto 没有进入一个新的局部变量的作用域，它可以跳转到任意可见标签处。

标签和没有内容的语句被称为空语句，它们不做任何操作。

break 被用来结束 while、 repeat、或 for 循环， 它将跳到循环外接着之后的语句运行：

	stat ::= break
break 跳出最内层的循环。

return 被用于从函数或是代码块（其实它就是一个函数） 中返回值。 函数可以返回不止一个值，所以 return 的语法为

	stat ::= return [explist] [‘;’]
return 只能被写在一个语句块的最后一句。 如果你真的需要从语句块的中间 return， 你可以使用显式的定义一个内部语句块， 一般写作 do return end。 可以这样写是因为现在 return 成了（内部）语句块的最后一句了。

### 3.3.5 – For 语句
for 有两种形式：一种是数字形式，另一种是通用形式。

数字形式的 for 循环，通过一个数学运算不断地运行内部的代码块。 下面是它的语法：

	stat ::= for Name ‘=’ exp ‘,’ exp [‘,’ exp] do block end
block 将把 name 作循环变量。 从第一个 exp 开始起，直到第二个 exp 的值为止， 其步长为第三个 exp 。 更确切的说，一个 for 循环看起来是这个样子

     for v = e1, e2, e3 do block end
这等价于代码：

     do
       local var, limit, step = tonumber(e1), tonumber(e2), tonumber(e3)
       if not (var and limit and step) then error() end
       var = var - step
       while true do
         var = var + step
         if (step >= 0 and var > limit) or (step < 0 and var < limit) then
           break
         end
         local v = var
         block
       end
     end
注意下面这几点：

所有三个控制表达式都只被运算一次， 表达式的计算在循环开始之前。 这些表达式的结果必须是数字。
var，limit，以及 step 都是一些不可见的变量。 这里给它们起的名字都仅仅用于解释方便。
如果第三个表达式（步长）没有给出，会把步长设为 1 。
你可以用 break 和 goto 来退出 for 循环。
循环变量 v 是一个循环内部的局部变量； 如果你需要在循环结束后使用这个值， 在退出循环前把它赋给另一个变量。
通用形式的 for 通过一个叫作 迭代器 的函数工作。 每次迭代，迭代器函数都会被调用以产生一个新的值， 当这个值为 nil 时，循环停止。 通用形式的 for 循环的语法如下：

	stat ::= for namelist in explist do block end
	namelist ::= Name {‘,’ Name}
这样的 for 语句

     for var_1, ···, var_n in explist do block end
它等价于这样一段代码：

     do
       local f, s, var = explist
       while true do
         local var_1, ···, var_n = f(s, var)
         if var_1 == nil then break end
         var = var_1
         block
       end
     end
注意以下几点：

explist 只会被计算一次。 它返回三个值， 一个 迭代器 函数， 一个 状态， 一个 迭代器的初始值。
f， s，与 var 都是不可见的变量。 这里给它们起的名字都只是为了解说方便。
你可以使用 break 来跳出 for 循环。
环变量 var_i 对于循环来说是一个局部变量； 你不可以在 for 循环结束后继续使用。 如果你需要保留这些值，那么就在循环跳出或结束前赋值到别的变量里去。
### 3.3.6 – 函数调用语句
为了允许使用函数的副作用， 函数调用可以被作为一个语句执行：

	stat ::= functioncall
在这种情况下，所有的返回值都被舍弃。 函数调用在 §3.4.10 中解释。

### 3.3.7 – 局部声明
局部变量可以在语句块中任何地方声明。 声明可以包含一个初始化赋值操作：

	stat ::= local namelist [‘=’ explist]
如果有初始化值的话，初始化赋值操作的语法和赋值操作一致 （参见 §3.3.3 ）。 若没有初始化值，所有的变量都被初始化为 nil。

一个代码块同时也是一个语句块（参见 §3.3.2）， 所以局部变量可以放在代码块中那些显式注明的语句块之外。

局部变量的可见性规则在 §3.5 中解释。

## 3.4 – 表达式
Lua 中有这些基本表达式：

	exp ::= prefixexp
	exp ::= nil | false | true
	exp ::= Numeral
	exp ::= LiteralString
	exp ::= functiondef
	exp ::= tableconstructor
	exp ::= ‘...’
	exp ::= exp binop exp
	exp ::= unop exp
	prefixexp ::= var | functioncall | ‘(’ exp ‘)’
数字和字面串在 §3.1 中解释； 变量在 §3.2 中解释； 函数定义在 §3.4.11 中解释； 函数调用在 §3.4.10 中解释； 表的构造在 §3.4.9 中解释。 可变参数的表达式写作三个点（'...'）， 它只能在有可变参数的函数中直接使用；这些在 §3.4.11 中解释。

二元操作符包含有数学运算操作符（参见 §3.4.1）， 位操作符（参见 §3.4.2）， 比较操作符（参见 §3.4.4）， 逻辑操作符（参见 §3.4.5）， 以及连接操作符（参见 §3.4.6）。 一元操作符包括负号（参见 §3.4.1）， 按位非（参见 §3.4.2）， 逻辑非（参见 §3.4.5）， 和取长度操作符（参见 §3.4.7）。

函数调用和可变参数表达式都可以放在多重返回值中。 如果函数调用被当作一条语句（参见 §3.3.6）， 其返回值列表被调整为零个元素，即抛弃所有的返回值。 如果表达式被用于表达式列表的最后（或是唯一的）一个元素， 那么不会做任何调整（除非表达式被括号括起来）。 在其它情况下， Lua 都会把结果调整为一个元素置入表达式列表中， 即保留第一个结果而忽略之后的所有值，或是在没有结果时， 补单个 nil。

这里有一些例子：

     f()                -- 调整为 0 个结果
     g(f(), x)          -- f() 会被调整为一个结果
     g(x, f())          -- g 收到 x 以及 f() 返回的所有结果
     a,b,c = f(), x     -- f() 被调整为 1 个结果 （c 收到 nil）
     a,b = ...          -- a 收到可变参数列表的第一个参数，
                        -- b 收到第二个参数（如果可变参数列表中
                        -- 没有实际的参数，a 和 b 都会收到 nil）
     
     a,b,c = x, f()     -- f() 被调整为 2 个结果
     a,b,c = f()        -- f() 被调整为 3 个结果
     return f()         -- 返回 f() 的所有返回结果
     return ...         -- 返回从可变参数列表中接收到的所有参数parameters
     return x,y,f()     -- 返回 x, y, 以及 f() 的所有返回值
     {f()}              -- 用 f() 的所有返回值创建一个列表
     {...}              -- 用可变参数中的所有值创建一个列表
     {f(), nil}         -- f() 被调整为一个结果
被括号括起来的表达式永远被当作一个值。 所以， (f(x,y,z)) 即使 f 返回多个值， 这个表达式永远是一个单一值。 （(f(x,y,z)) 的值是 f 返回的第一个值。 如果 f 不返回值的话，那么它的值就是 nil 。）

### 3.4.1 – 数学运算操作符
Lua 支持下列数学运算操作符：

+: 加法
-: 减法
*: 乘法
/: 浮点除法
//: 向下取整除法
%: 取模
^: 乘方
-: 取负
除了乘方和浮点除法运算， 数学运算按如下方式工作： 如果两个操作数都是整数， 该操作以整数方式操作且结果也将是一个整数。 否则，当两个操作数都是数字或可以被转换为数字的字符串 （参见 §3.4.3）时， 操作数会被转换成两个浮点数， 操作按通常的浮点规则（一般遵循 IEEE 754 标准） 来进行，结果也是一个浮点数。

乘方和浮点除法 （/） 总是把操作数转换成浮点数进行，其结果总是浮点数。 乘方使用 ISO C 函数 pow， 因此它也可以接受非整数的指数。

向下取整的除法 （//） 指做一次除法，并将商圆整到靠近负无穷的一侧， 即对操作数做除法后取 floor 。

取模被定义成除法的余数，其商被圆整到靠近负无穷的一侧（向下取整的除法）。

对于整数数学运算的溢出问题， 这些操作采取的策略是按通常遵循的以 2 为补码的数学运算的 环绕 规则。 （换句话说，它们返回其运算的数学结果对 264 取模后的数字。）

### 3.4.2 – 位操作符
Lua 支持下列位操作符：

&: 按位与
|: 按位或
~: 按位异或
>>: 右移
<<: 左移
~: 按位非
所有的位操作都将操作数先转换为整数 （参见 §3.4.3）， 然后按位操作，其结果是一个整数。

对于右移和左移，均用零来填补空位。 移动的位数若为负，则向反方向位移； 若移动的位数的绝对值大于等于 整数本身的位数，其结果为零 （所有位都被移出）。

### 3.4.3 – 强制转换
Lua 对一些类型和值的内部表示会在运行时做一些数学转换。 位操作总是将浮点操作数转换成整数。 乘方和浮点除法总是将整数转换为浮点数。 其它数学操作若针对混合操作数 （整数和浮点数）将把整数转换为浮点数； 这一点被称为 通常规则。 C API 同样会按需把整数转换为浮点数以及 把浮点数转换为整数。 此外，字符串连接操作除了字符串，也可以接受数字作为参数。

当操作需要数字时，Lua 还会把字符串转换为数字。

当把一个整数转换为浮点数时， 若整数值恰好可以表示为一个浮点数，那就取那个浮点数。 否则，转换会取最接近的较大值或较小值来表示这个数。 这种转换是不会失败的。

将浮点数转为整数的过程会检查 浮点数能否被准确的表达为一个整数 （即，浮点数是一个整数值且在整数可以表达的区间）。 如果可以，结果就是那个数，否则转换失败。

从字符串到数字的转换过程遵循以下流程： 首先，遵循按 Lua 词法分析器的规则分析语法来转换为对应的 整数或浮点数。 （字符串可以有前置或后置的空格以及一个符号。） 然后，结果数字再按前述规则转换为所需要的类型（浮点或整数）。

从数字转换为字符串使用非指定的人可读的格式。 若想完全控制数字到字符串的转换过程， 可以使用字符串库中的 format 函数 （参见 string.format）。

### 3.4.4 – 比较操作符
Lua 支持下列比较操作符：

==: 等于
~=: 不等于
<: 小于
>: 大于
<=: 小于等于
>=: 大于等于
这些操作的结果不是 false 就是 true。

等于操作 （==）先比较操作数的类型。 如果类型不同，结果就是 false。 否则，继续比较值。 字符串按一般的方式比较。 数字遵循二元操作的规则： 如果两个操作数都是整数， 它们按整数比较； 否则，它们先转换为浮点数，然后再做比较。

表，用户数据，以及线程都按引用比较： 只有两者引用同一个对象时才认为它们相等。 每次你创建一个新对象（一张表，一个用户数据，或一个线程）， 新对象都一定和已有且存在的对象不同。 相同引用的闭包一定相等。 有任何可察觉的差异（不同的行为，不同的定义）一定不等。

你可以通过使用 "eq" 元方法（参见 §2.4） 来改变 Lua 比较表和用户数据时的方式。

等于操作不会将字符串转换为数字，反之亦然。 即，"0"==0 结果为 false， 且 t[0] 与 t["0"] 指代着表中的不同项。

~= 操作完全等价于 (==) 操作的反值。

大小比较操作以以下方式进行。 如果参数都是数字， 它们按二元操作的常规进行。 否则，如果两个参数都是字符串， 它们的值按当前的区域设置来比较。 再则，Lua 就试着调用 "lt" 或是 "le" 元方法 （参见 §2.4）。 a > b 的比较被转译为 b < a， a >= b 被转译为 b <= a。

### 3.4.5 – 逻辑操作符
Lua 中的逻辑操作符有 and， or，以及 not。 和控制结构（参见 §3.3.4）一样， 所有的逻辑操作符把 false 和 nil 都作为假， 而其它的一切都当作真。

取反操作 not 总是返回 false 或 true 中的一个。 与操作符 and 在第一个参数为 false 或 nil 时 返回这第一个参数； 否则，and 返回第二个参数。 或操作符 or 在第一个参数不为 nil 也不为 false 时， 返回这第一个参数，否则返回第二个参数。 and 和 or 都遵循短路规则； 也就是说，第二个操作数只在需要的时候去求值。 这里有一些例子：

     10 or 20            --> 10
     10 or error()       --> 10
     nil or "a"          --> "a"
     nil and 10          --> nil
     false and error()   --> false
     false and nil       --> false
     false or nil        --> nil
     10 and 20           --> 20
（在这本手册中， --> 指前面表达式的结果。）

### 3.4.6 – 字符串连接
Lua 中字符串的连接操作符写作两个点（'..'）。 如果两个操作数都是字符串或都是数字， 连接操作将以 §3.4.3 中提到的规则把其转换为字符串。 否则，会调用元方法 __concat （参见 §2.4）。

### 3.4.7 – 取长度操作符
取长度操作符写作一元前置符 #。 字符串的长度是它的字节数（就是以一个字符一个字节计算的字符串长度）。

程序可以通过 __len 元方法（参见 §2.4） 来修改对字符串类型外的任何值的取长度操作行为。

如果 __len 元方法没有给出， 表 t 的长度只在表是一个 序列 时有定义。 序列指表的正数键集等于 {1..n} ， 其中 n 是一个非负整数。 在这种情况下，n 是表的长度。 注意这样的表

     {10, 20, nil, 40}
不是一个序列，因为它有键 4 却没有键 3。 （因此，该表的正整数键集不等于 {1..n} 集合，故而就不存在 n。） 注意，一张表是否是一个序列和它的非数字键无关。

### 3.4.8 – 优先级
Lua 中操作符的优先级写在下表中，从低到高优先级排序：

     or
     and
     <     >     <=    >=    ~=    ==
     |
     ~
     &
     <<    >>
     ..
     +     -
     *     /     //    %
     unary operators (not   #     -     ~)
     ^
通常， 你可以用括号来改变运算次序。 连接操作符 ('..') 和乘方操作 ('^') 是从右至左的。 其它所有的操作都是从左至右。

### 3.4.9 – 表构建
表构造子是一个构造表的表达式。 每次构造子被执行，都会构造出一张新的表。 构造子可以被用来构造一张空表， 也可以用来构造一张表并初始化其中的一些域。 一般的构造子的语法如下

	tableconstructor ::= ‘{’ [fieldlist] ‘}’
	fieldlist ::= field {fieldsep field} [fieldsep]
	field ::= ‘[’ exp ‘]’ ‘=’ exp | Name ‘=’ exp | exp
	fieldsep ::= ‘,’ | ‘;’
每个形如 [exp1] = exp2 的域向表中增加新的一项， 其键为 exp1 而值为 exp2。 形如 name = exp 的域等价于 ["name"] = exp。 最后，形如 exp 的域等价于 [i] = exp ， 这里的 i 是一个从 1 开始不断增长的数字。 这这个格式中的其它域不会破坏其记数。 举个例子：

     a = { [f(1)] = g; "x", "y"; x = 1, f(x), [30] = 23; 45 }
等价于

     do
       local t = {}
       t[f(1)] = g
       t[1] = "x"         -- 1st exp
       t[2] = "y"         -- 2nd exp
       t.x = 1            -- t["x"] = 1
       t[3] = f(x)        -- 3rd exp
       t[30] = 23
       t[4] = 45          -- 4th exp
       a = t
     end
构造子中赋值的次序未定义。 （次序问题只会对那些键重复时的情况有影响。）

如果表单中最后一个域的形式是 exp ， 而且其表达式是一个函数调用或者是一个可变参数， 那么这个表达式所有的返回值将依次进入列表 （参见 §3.4.10）。

初始化域表可以在最后多一个分割符， 这样设计可以方便由机器生成代码。

### 3.4.10 – 函数调用
Lua 中的函数调用的语法如下：

	functioncall ::= prefixexp args
函数调用时， 第一步，prefixexp 和 args 先被求值。 如果 prefixexp 的值的类型是 function， 那么这个函数就被用给出的参数调用。 否则 prefixexp 的元方法 "call" 就被调用， 第一个参数是 prefixexp 的值， 接下来的是原来的调用参数 （参见 §2.4）。

这样的形式

	functioncall ::= prefixexp ‘:’ Name args
可以用来调用 "方法"。 这是 Lua 支持的一种语法糖。 像 v:name(args) 这个样子， 被解释成 v.name(v,args)， 这里的 v 只会被求值一次。

参数的语法如下：

	args ::= ‘(’ [explist] ‘)’
	args ::= tableconstructor
	args ::= LiteralString
所有参数的表达式求值都在函数调用之前。 这样的调用形式 f{fields} 是一种语法糖用于表示 f({fields})； 这里指参数列表是一个新创建出来的列表。 而这样的形式 f'string' （或是 f"string" 亦或是 f[[string]]） 也是一种语法糖，用于表示 f('string')； 此时的参数列表是一个单独的字符串。

return functioncall 这样的调用形式将触发一次 尾调用。 Lua 实现了 完全尾调用（或称为 完全尾递归）： 在尾调用中， 被调用的函数重用调用它的函数的堆栈项。 因此，对于程序执行的嵌套尾调用的层数是没有限制的。 然而，尾调用将删除调用它的函数的任何调试信息。 注意，尾调用只发生在特定的语法下， 仅当 return 只有单一函数调用作为参数时才发生尾调用； 这种语法使得调用函数的所有结果可以完整地返回。 因此，下面这些例子都不是尾调用：

     return (f(x))        -- 返回值被调整为一个
     return 2 * f(x)
     return x, f(x)       -- 追加若干返回值
     f(x); return         -- 返回值全部被舍弃
     return x or f(x)     -- 返回值被调整为一个
### 3.4.11 – 函数定义
函数定义的语法如下：

	functiondef ::= function funcbody
	funcbody ::= ‘(’ [parlist] ‘)’ block end
另外定义了一些语法糖简化函数定义的写法：

	stat ::= function funcname funcbody
	stat ::= local function Name funcbody
	funcname ::= Name {‘.’ Name} [‘:’ Name]
该语句

     function f () body end
被转译成

     f = function () body end
该语句

     function t.a.b.c.f () body end
被转译成

     t.a.b.c.f = function () body end
该语句

     local function f () body end
被转译成

     local f; f = function () body end
而不是

     local f = function () body end
（这个差别只在函数体内需要引用 f 时才有。）

一个函数定义是一个可执行的表达式， 执行结果是一个类型为 function 的值。 当 Lua 预编译一个代码块时， 代码块作为一个函数，整个函数体也就被预编译了。 那么，无论何时 Lua 执行了函数定义， 这个函数本身就进行了 实例化（或者说是 关闭了）。 这个函数的实例（或者说是 闭包）是表达式的最终值。

形参被看作是一些局部变量， 它们将由实参的值来初始化：

	parlist ::= namelist [‘,’ ‘...’] | ‘...’
当一个函数被调用， 如果函数并非一个 可变参数函数， 即在形参列表的末尾注明三个点 ('...')， 那么实参列表就会被调整到形参列表的长度。 变长参数函数不会调整实参列表； 取而代之的是，它将把所有额外的参数放在一起通过 变长参数表达式传递给函数， 其写法依旧是三个点。 这个表达式的值是一串实参值的列表， 看起来就跟一个可以返回多个结果的函数一样。 如果一个变长参数表达式放在另一个表达式中使用， 或是放在另一串表达式的中间， 那么它的返回值就会被调整为单个值。 若这个表达式放在了一系列表达式的最后一个， 就不会做调整了 （除非这最后一个参数被括号给括了起来）。

我们先做如下定义，然后再来看一个例子：

     function f(a, b) end
     function g(a, b, ...) end
     function r() return 1,2,3 end
下面看看实参到形参数以及可变长参数的映射关系：

     CALL            PARAMETERS
     
     f(3)             a=3, b=nil
     f(3, 4)          a=3, b=4
     f(3, 4, 5)       a=3, b=4
     f(r(), 10)       a=1, b=10
     f(r())           a=1, b=2
     
     g(3)             a=3, b=nil, ... -->  (nothing)
     g(3, 4)          a=3, b=4,   ... -->  (nothing)
     g(3, 4, 5, 8)    a=3, b=4,   ... -->  5  8
     g(5, r())        a=5, b=1,   ... -->  2  3
结果由 return 来返回（参见 §3.3.4）。 如果执行到函数末尾依旧没有遇到任何 return 语句， 函数就不会返回任何结果。

关于函数可返回值的数量限制和系统有关。 这个限制一定大于 1000 。

冒号 语法可以用来定义 方法， 就是说，函数可以有一个隐式的形参 self。 因此，如下语句

     function t.a.b.c:f (params) body end
是这样一种写法的语法糖

     t.a.b.c.f = function (self, params) body end
## 3.5 – 可见性规则
Lua 语言有词法作用范围。 变量的作用范围开始于声明它们之后的第一个语句段， 结束于包含这个声明的最内层语句块的最后一个非空语句。 看下面这些例子：

     x = 10                -- 全局变量
     do                    -- 新的语句块
       local x = x         -- 新的一个 'x', 它的值现在是 10
       print(x)            --> 10
       x = x+1
       do                  -- 另一个语句块
         local x = x+1     -- 又一个 'x'
         print(x)          --> 12
       end
       print(x)            --> 11
     end
     print(x)              --> 10 （取到的是全局的那一个）
注意这里，类似 local x = x 这样的声明， 新的 x 正在被声明，但是还没有进入它的作用范围， 所以第二个 x 指向的是外面一层的变量。

因为有这样一个词法作用范围的规则， 局部变量可以被在它的作用范围内定义的函数自由使用。 当一个局部变量被内层的函数中使用的时候， 它被内层函数称作 上值，或是 外部局部变量。

注意，每次执行到一个 local 语句都会定义出一个新的局部变量。 看看这样一个例子：

     a = {}
     local x = 20
     for i=1,10 do
       local y = 0
       a[i] = function () y=y+1; return x+y end
     end
这个循环创建了十个闭包（这指十个匿名函数的实例）。 这些闭包中的每一个都使用了不同的 y 变量， 而它们又共享了同一份 x。

--------------------------

\page "事件库基础规则"

# 概述
事件库遵循一切皆事件的原则，所有逻辑均有事件组合而成。

# 事件类型
事件库类型如下
- 即时事件  如果是C#事件，且事件执行未成功，调用方也会被迫中止. 所有c#的即时事件中，除非必要，所有事件需要标明执行事件成功。
- 任务事件  Task域下 返回事件ID
- 监听事件  Listen域下，立即返回事件ID 监听事件最后一个参数为收到监听会触发的函数，如果没有此参数，Listen为一次性监听，此时和任务事件表现一致

# 事件挂起
以下Api调用时会触发事件挂起
- Sleep
- Wait 
- WaitSelect 
- WaitAlways
- WaitParallel

# 脚本结构
事件库脚本均为沙盒结构，必须的函数是`main`，`main`函数中执行事件脚本的具体逻辑。 `main`的返回值即为事件脚本的返回值， 第一个返回值为事件处理结果(true/false)。
事件脚本中可以有一个可选函数`clean`，`clean`的作用为为事件脚本结束时的一些清理工作，该函数无论`main`中执行结果如何，均会在脚本结束时调用。以下是一个脚本示例：

    function main()
      print('welcome event script')
    end

    function clean()
      print('clean now ....')
    end


# 模块介绍
- 客户端模块 客户端模块调用规范，事件表中使用client.事件路径，启动客户端目录event_script/Client的指定事件