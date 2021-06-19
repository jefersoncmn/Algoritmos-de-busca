<h1 align="center">First work of the Intelligent Systems Fundaments</h1>

### Sobre

O trabalho consiste em implementar um sistema de navegação automática de um agente
utilizando o algoritmo de busca em largura, profundidade, gulosa e A*.
O agente deve ser capaz de calcular automaticamente a melhor rota para chegar a
qualquer ponto de um ambiente representado através de um grafo que conecta com seus
vértices as áreas, locais ou partes do caminho onde o agente pode navegar. O grafo
representa então um cenário fictício que você deve criar, onde o agente irá tentar encontrar
um prêmio (estado objetivo) que se encontra em algum local diferente de onde o agente
inicia no ambiente (nó inicial), o grafo deve ter pelo menos 30 vértices. Além do prêmio
final durante o percurso o agente também deve coletar recompensas que estão espalhadas
no mapa. Os algoritmos de busca cega devem pegar as recompensas que aparecerem nos
vértices visitados durante a verificação do caminho que leva ao prêmio final. Já os
algoritmos com heurística devem ter em sua heurística uma forma de avaliar se é
compensador deslocar da rota que leva para o prêmio final para pegar recompensas que
estejam em vértices próximos durante esse caminho.
O ambiente por onde o agente irá navegar é formado por diversos tipos de terrenos e em
cada tipo de terreno o agente tem um grau de dificuldade diferente para andar. Por
exemplo, o agente consegue passar facilmente por um terreno solido e plano, porem terá
dificuldade para andar em um terreno rochoso ou um pântano.
A melhor rota para chegar a um determinado ponto do ambiente é a rota que tem o menor
custo.
