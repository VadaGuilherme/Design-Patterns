# Design Patterns C#
Este trabalho foi criado com o intuito de aprender e tentar fixar na cabeça, de maneira simples os 22 padrões de projeto. Aqui criei um exmplo simples para cada pattern e irei explicá-los detalhadamente.

# Introdução
<p>Padrões de projeto são <b>soluções</b> muito bem testadas para resolver problemas comuns em projetos de softwares, são <b>diretries sobre como lidar com algum determinado problema</b>, basicamente, são soluções utilizando os princípio de orientação a objetos, os padrões de projeto definem uma linguagem única entre os desenvolvedores, pois quando você se depara com um problema, você pode sugerir "isso conseguimos resolver com <b>strategy</b>" e todos os outros desenvolvedores iram entender a ideia da solução proposta.</p>
<p>Os patterns são classificados em três categorias, sejam elas <b>criacionais</b>, <b>estruturais</b> e <b>comportamentais</b> onde</p>
<p><b>Criacionais</b>: Refere-se a mecanismos para a criação de objetos, tem como objetivo abstrair a instancia dos objetos, de maneira que permita a flexibilidade e reutilização do código existente</p>
<p><b>Estruturais</b>: Refere-se a mecanismos para montar objetos em estruturas maiores, organizando a estrutura das classes e o relacionamento entre elas, mantendo as estruturas flexiveis e eficientes</p>
<p><b>Comportamentais</b>: Refere-se a mecanismos para atribuir responsabilidades entre os objetos, definindo como os objetos devem se comportar e se comunicar</p>
 
 <p>De maneira geral, o padrão não é um trecho de código específico e sim um conceito geral, onde você pode seguir os detalhes e implementar no projeto de maneira que faça sentido a realidade do seu sistema</p>
  
 # Atenção
 <p>De qualquer forma, temos que ter em mente que adicionar um padrão de projeto é adicionar complexidade, e complexidade é custo!!</p>
<p>Nas linguagens mais modernas por exemplo, conseguimos resolver um problema que seria solucionado com o pattern strategy, simplesmente utilizando uma função anônima(dependendo do caso), deste modo antes de escolher um pattern deve-se compreender muito bem o problema que você quer resolver, antes de sair implementando com um pattern popular.</p>
<p>Os padrões de projeto podem ser facilmente encontrados na internet(como este por exemplo) mas eles somente nos explicam <b>o que</b> ele é e <b>como</b> implementar, mas eles não nos dizem <b>quando</b> e o <b>por que</b> implementar, as vezes você esta trabalhando com uma linguagem que não tem suporte as funções anônimas e por isso uma das melhores saídas, pode ser o pattern strategy

<p>Devemos implementar os patterns para resolver os nossos problemas e não criar problemas para que seja possível resolver com o pattern</p>
<p>
<img src="https://github.com/Gustavo-Braga/Design-Patterns/blob/feature/CreateReadme/src/BackEnd/image1.jpg">
 <h6><align="center">(fonte: <a href="https://refactoring.guru/design-patterns/criticism" target="_blank">
refactoring.guru</a>)</h6>


</p>

# Tipos de padrões de projeto
<ul>Criacional
  <li>Singleton</li>
  <li>Protótipo(prototype)</li>
  <li>Constutor(builder)</li>
  <li>Método de Fábrica(factory method)</li>
  <li>Fábrica Abstrata(abstract factory)</li>
</ul>
<ul>Estrutural
  <li>Adaptador(adapter)</li>
  <li>Decorador(decorator)</li>
  <li>Ponte(bridge)</li>
  <li>Fachada(facade)</li>
  <li>Proxy</li>
  <li>Composto(composite)</li>
  <li>Flyweight</li>
</ul>

<ul>Comportamental
  <li>Mediador(mediator)</li>
  <li>Observador(observer)</li>
  <li>Cadeia de Responsabilidade(chain of responsibility)</li>
  <li>Comando(command)</li>
  <li>Iterador(iterator)</li>
  <li>Lembrança(memento)</li>
  <li>Método de Modelo(template method)</li>
  <li>Estado(stade)</li>
  <li>Estratégia(strategy)</li>
  <li>Visitante(visitor)</li>
</ul>

# Singleton

<p><b>O que é</b>: Singleton é um padrão de design criacional que lhe permite que apenas uma instância desse tipo de objeto exista.</p>
<p><b>Exemplo do mundo real</b>:</p>

> <p>Só pode haver um presidente de um país. O mesmo presidente deve ser acionado sempre que o dever exigir. O presidente é singleton.</p>
 
 <p><b>Problema</b>:</p>
 <p>Certifique-se de que uma classe possua uma única instância, o motivo mais comum para isso, seria controlar o acesso a algum recurso compartilhado, por exemplo, uma classe de banco de dados</p>
 
<p>Para o nosso exemplo, foi criado uma classe de repositório onde só pode haver uma instância do objeto, para esta classe é necessário informar o nome da tabela que o repositório ira atuar. Para esta classe também foi implementado o thread safe para não quebrar a funcionalidade caso seja chamado de vários threads simultaneamente</p>

<p><b>Solução</b>:</p>
<p>Torne o construtor padrão privado, para impedir que outros objetos utilizem o operador "new"</p>

```c#
      private ProductRepository(string tableName)
      {
          TableName = tableName;
      }
      
      public string TableName { get; set; }
      public static ProductRepository _instance;
      public static readonly object _lock = new object();
```

<p>Crie um método de criação estático que atua como construtor, este método deve chamar o construtor privado e salvar em um campo estático, todas as chamadas a seguir devem retornar o objeto estático</p>
 
 ```c#
 public static ProductRepository GetInstance(string tableName)
        {
            if (_instance == null)
                lock (_lock)
                    _instance = new ProductRepository(tableName);

            return _instance;
        }
```

<p>Com isto implementado, todas as chamadas ao ProductRepository ira retornar a mesma instancia salva na variável "_instance", deste modo, para realizar a chamada ao método, fica da seguinte maneira:</p>

 ```c#
  var repository = ProductRepository.GetInstance("Product");
  Console.WriteLine($"Somente uma instância de ProductRepository: {repository.TableName}");
```

<p><b>Saída</b>:</p>

> <p>Hello Word</p>
> <p>Somente uma instância de ProductRepository: Product</p>


<p>Use o padrão singleton quando, necessitar de somente uma instância disponível para as classes do sistema, por exemplo, uma classe de banco de dados.</p>  

# Protótipo(prototype)

<p><b>O que é</b>: protótipo é um padrão de design criacional que permite criar novos objetos a partir de um modelo original, permite 
copiar objetos sem tornar o código dependente de suas classes</p>
 
<p><b>Exemplo do mundo real:</p></b>

> Protóripos são criados para possibilitar os testes antes de iniciar a produção em massa de um determinado produto, no entando esses protótipos não participam de nenhuma produção real, desempenha somente um papel passivo.

<p><b>Problema</b>:</p> Digamos que você tenha um objeto e você precisa criar uma cópia exata dele. Você precisaria criar um novo objeto da mesma classe e percorrer todos os campos do objeto original para o novo objeto, porém, criar um objeto "de fora" nem sempre é possível, pois o objeto original pode conter campos privados que seriam invisíveis para quem está realizando a operação de "cópia" do objeto.
 
 <p>Para o nosso exemplo, foi criado uma classe simples de funcionário, onde recebe duas interfaces, a interface IEmployee que é utilizada somente para teste, mostrando uma alternativa para realizar a chamada do método clone, ou seja, pode ser desconsiderada, e a interface ICloneable que é disponibilizada pela Microsoft através da biblioteca System, esta interface segue o exemplo "comum" para implementação do padrão de protótipo
 
 <p><b>Solução</b>:</p>
<p><b>Implementação alternativa</b>:</p>
<p>Crie um interface e atribua um método para ser possível realizar a clonagem do objeto, no nosso caso, a interface IEmployee</p>

 ```c#
    public interface IEmployee
    {
        //método utilizado para teste, pois em C# existe a interface 
        //ICloneable que subistituiria o uso dessa interface
        IEmployee CloneEmployee();
    }
```

<p>Na classe em que deseja realizar a clonagem, herde a interface IEmployee</p>

```c#
    public class Developer : IEmployee
    {
        public Developer(string name, double salary, IEnumerable<string> languages)
        {
            Name = name;
            Salary = salary;
            Languages = languages;
        }
        
        public string Name { get; set; }
        private double Salary { get; set; }
        public IEnumerable<string> Languages { get; set; }
    }
```

<p>Com isso, você é forçado a implementar o método CloneEmployee, que fica da seguinte maneira</p>

```c#
        public IEmployee CloneEmployee()
        {
            return (Developer)MemberwiseClone();
        }
```

<p>Após a implementação do método, é só realizar a chamada do mesmo</p>

```c#
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var developer = new Developer("Fulano", new Random().NextDouble(), new List<string> { "C#", "JS" });
            var prototype = developer.CloneEmployee();
            Console.WriteLine(prototype.ToString());
            developer.Name = "Cicrano";
            Console.WriteLine(prototype.ToString());
        }
```

<p>Agora, irei mostrar sem a utilização da interface IEmployee e sim com a utilização da interface ICloneable</p>
<p>Na classe em que deseja realizar a clonagem, herde a interface ICloneable</p>

```c#
    public class Developer : ICloneable
    {
        public Developer(string name, double salary, IEnumerable<string> languages)
        {
            Name = name;
            Salary = salary;
            Languages = languages;
        }
        
        public string Name { get; set; }
        private double Salary { get; set; }
        public IEnumerable<string> Languages { get; set; }
    }
    
```

<p>Com isso, você é forçado a implementar o método Clone, que fica da seguinte maneira</p>

```c#
        public object Clone()
        {
            return MemberwiseClone();
        }
```

<p>Após a implementação do método, é só realizar a chamada do mesmo</p>

```c#
        static void Main(string[] args)
        {
            var developer = new Developer("Fulano", new Random().NextDouble(), new List<string> { "C#", "JS" });
            var prototype2 = (Developer)developer.Clone();
            Console.WriteLine(prototype2.ToString());
            prototype2.Name = "Beltrano";
            Console.WriteLine(prototype2.ToString());
            Console.ReadKey();
        }
```

<p>Em c# a classe Object(que é a classe base de todas as classes) nos fornece o método MemberwiseClone, que criar uma cópia superficial o objeto.</p>

<p><b>Saída</b>:</p>

> <p>Hello World!</p>
> <p>Fulano, 0,602327118442546, C#, JS</p>
> <p>Fulano, 0,602327118442546, C#, JS</p>
> <p>Cicrano, 0,602327118442546, C#, JS</p>
> <p>Beltrano, 0,602327118442546, C#, JS</p>


<p>Use o padrão protóripo quando necessitar copiar objetos sem depender da sua classe concreta. O padrão protótipo torna bem mais simples a criação de novos objetos complexos</p>  

# Constutor(builder)

<p><b>O que é</b>: Contrutor é um padrão de design criacional que permite construir objetos complexos passo a passo, o padrão construtor descreve uma maneira simples de separar o objeto de sua construção
 
<p><b>Problema</b>:</p> Imagine que você tenha um objeto complexo, em que a inicialização é trabalhosa e precisa de vários passos para que seja possível montar o objeto, o código de inicialização é oculto dentro do construtor e há muitos parametros de entrada, por exemplo, vamos pensar no objeto "House". Para construir uma casa simples, você precisa construir quatro paredes, um piso, instalar uma porta, encaixar um par de janelas e construir um telhado. Mas e se você quiser uma casa maior e mais brilhante, com um quintal e outras coisas? A solução mais simples, seria estender a "House" e criar um conjunto de subclasses para cobrir todas as combinações de parâmetros, porém ja conseguimos imaginar que iria ter um aumento considerável nas subclasses e um aumento considerável nos parâmetros do constutor.


<p>Para o nosso exemplo, estarei mostrando duas alternativas para implementar o padrão construtor, a primeira, utilizando a classe de "Diretor" e a segunda sem a utilização desta classe, no nosso caso trata-se da criação de um veículo, onde é separado o construtor em vários métodos a fins de que se possa montar o objeto veículo de diferentes formas

 <p><b>Solução</b>:</p>
<p>Deve ser implementado uma interface <b>IBuilder</b> para que nela possa ser extraido todos os métodos referente a criação do objeto. A classe concreta deve herdar desta interface e nos métodos estará a lógica para a criação das partes específicas. A classe de <b>diretor</b> controla o algoritmo para corder de execução das etapas de criação</p>

<p><b>Alternativa com diretor</b>:</p>
<p>Implemente a interface IBuilder, no nosso caso IVehicleBuilder</p>
 
 ```c#
     public interface IVehicleBuilder
    {
        void SetModel();
        void SetYear();
        void SetColor();
        void SetAcessories();
    }
 ```
 
  <p>Após isso, crie a classe de diretor, a mesma deve conter um atributo do tipo IVehicleBuilder</p>
  
   
 ```c#
  public class VehicleCreator 
    {
        private readonly IVehicleBuilder vehicleBuilder;

        public VehicleCreator(IVehicleBuilder vehicleBuilder)
        {
            this.vehicleBuilder = vehicleBuilder;
        }
    }
 ```
 
<p>Feito isso, nesta classe podemos definir o algoritmo para ordem das etapas de criação do objeto. Implementaremos dois métodos sendo eles, CreateVehicleCaracteristics onde irá carregar as características padrões do veículo e o segundo, CreateVehicleAcessories onde irá carregar os acessórios do veículo</p>

 ```c#
        public void CreateVehicleCaracteristics()
        {
            vehicleBuilder.SetModel();
            vehicleBuilder.SetYear();
            vehicleBuilder.SetColor();
        }

        public void CreateVehicleAcessories()
        {
            vehicleBuilder.SetAcessories();
        }
 ```
 
<p>Agora iremos criar a classe Vehicle que será responsável por representar o nosso veículo</p>

 ```c#
    public class Vehicle
    {
        public Vehicle()
        {
            Acessories = new List<string>();
        }

        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public IEnumerable<string> Acessories { get; set; }
    }
 ```

<p>Tudo certo até aqui, neste momento iremos criar a classe CarBuilder que irá representar o nosso veículo, expecíficamente um carro. A classe CarBuilder deve conter um atributo privado do tipo Vehicle e deve herdar da nossa interface de builder, a interface IVehicleBuilder, onde irá forçar implementar seus métodos, nesses métodos ficará a lógica para a criação do nosso carro.</p>

<p>É recomendado que as classes concretas que irão representar o nosso produto, tenha um método para recuperar o objeto, pois pode haver cenários em que vários tipos de construtores retornem diferentes tipos de objetos e por isso este método nao é declarado na interface </p>

 ```c#
public class CarBuilder: IVehicleBuilder
    {
        private Vehicle Vehicle = new Vehicle();

        public void SetModel()
        {
            Vehicle.Model = "Sedan";
        }

        public void SetYear()
        {
            Vehicle.Year = 2020;
        }

        public void SetColor()
        {
            Vehicle.Color = "Vermelho";
        }

        public void SetAcessories()
        {
            Vehicle.Acessories = new List<string> { "Neon", "Capa para bancos", "Alarme"};
        }

        public Vehicle GetVehicle()
        {
            return Vehicle;
        }

        public override string ToString()
        {
            return $"Modelo: {Vehicle.Model}, Ano: {Vehicle.Year}, Cor: {Vehicle.Color}, Acessórios: {string.Join(", ", Vehicle.Acessories)}";
        }

    }
 ```
 
 <p>Tudo pronto para o nosso primeiro exemplo, feito isso é só chamar a classe diretor(VehicleCreator) passando o nosso objeto builder(CarBuilder). Assim conseguiremos utilizar os métodos disponíveis no diretor para criar o nosso carro em etapas</p>
 
 ```c#
         private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var carBuilder = new CarBuilder();
            var car = new VehicleCreator(carBuilder);
            car.CreateVehicleCaracteristics();
            Console.WriteLine(carBuilder.ToString());

            car.CreateVehicleAcessories();
            Console.WriteLine(carBuilder.ToString());
            
        }
 ```
 
 <p><b>Saída</b>:</p>

> <p>Hello World!</p>
> <p>Modelo: Sedan, Ano: 2020, Cor: Vermelho, Acessórios:</p>
> <p>Modelo: Sedan, Ano: 2020, Cor: Vermelho, Acessórios: Neon, Capa para bancos, Alarme</p>
 
 
 <p><b>Alternativa sem diretor(consequentemente mais fácil)</b>:</p>
 <p>Para este segundo exemplo, iremos criar a classe TruckBuilder que irá representar o nosso veículo, expecíficamente um caminhão. A classe TruckBuilder deve conter um atributo privado do tipo Vehicle e <b>não</b> precisa herdar a interface IVehicleBuilder(para este exemplo). A classe TruckBuilder irá conter os métodos para criar o objeto do tipo Vehicle e em todos os métodos deverá retornar a própria classe, com isso fica mais fácil e intuitivo para realizar a montagem do objeto
 
```c#
public class TruckBuilder
    {
        private Vehicle Vehicle = new Vehicle();

        public TruckBuilder SetModel(string model)
        {
            Vehicle.Model = model;
            return this;
        }

        public TruckBuilder SetYear(int year)
        {
            Vehicle.Year = year;
            return this;
        }

        public TruckBuilder SetColor(string color)
        {
            Vehicle.Color = color;
            return this;
        }

        public TruckBuilder SetAcessories(IEnumerable<string> acessories)
        {
            Vehicle.Acessories = new List<string> { "Geladeira", "Capa para bancos", "Alarme" };
            Vehicle.Acessories = acessories;
            return this;
        }

        public Vehicle GetVehicle()
        {
            return Vehicle;
        }

        public override string ToString()
        {
            return $"Modelo: {Vehicle.Model}, Ano: {Vehicle.Year}, Cor: {Vehicle.Color}, Acessórios: {string.Join(", ", Vehicle.Acessories)}";
        }
    }

 ```
 
<p>Implementado a classe TruckBuilder é só chama-la e executar seus métodos</p>

 ```c#
      private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var truckBuilder = new TruckBuilder()
                .SetModel("FH-420")
                .SetColor("Black")
                .SetYear(2020);

            Console.WriteLine(truckBuilder.ToString());

            truckBuilder.SetAcessories(new List<string> { "Geladeira", "Capa para bancos", "Alarme" });
            Console.WriteLine(truckBuilder.ToString());
        }
 ```
 
 <p><b>Saída</b>:</p>

> <p>Hello World!</p>
> <p>Modelo: FH-420, Ano: 2020, Cor: Black, Acessórios:</p>
> <p>Modelo: FH-420, Ano: 2020, Cor: Black, Acessórios: Geladeira, Capa para bancos, Alarme</p>
 
<p>Use o padrão construtor quando deseja se livrar de construtores com 10 parâmetros opcionais. Quando desejar que seu código possa criar representações diferentes sobre algum produto(como o nosso exemplo, carro e caminhão). O padrão construtor pode ser aplicado quando a construção de várias representações do produto envolve etapas semelhantes que diferem apenas nos detalhes.</p>
