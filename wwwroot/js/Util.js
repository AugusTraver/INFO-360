const Hidden = document.getElementById('Hidden')

let ListaDeAlarmas = [] 
function NuevaAlarma(){
  Hidden.hidden = false
  document.getElementById('AlarmaCreada').hidden = true
}
function AgregarAlarma(){
    if(document.getElementById("AlarmaTime").value == null || document.getElementById("NombreAlarma").value == null ||
    document.getElementById("AlarmaTime").value == '' || document.getElementById("NombreAlarma").value == '' ){
      document.getElementById('ErrorCrearAlarma').innerHTML = "Ocurrio un error: revisa que todos los campos esten completos"    
    }else{
    document.getElementById('ErrorCrearAlarma').hidden = true
    Hidden.hidden = true
    document.getElementById('AlarmaCreada').innerHTML = "La alarma ha sido creada con exito"
    document.getElementById('AlarmaCreada').hidden = false
    const Alarma = new Date(document.getElementById("AlarmaTime").value) 
    const AlarmaNombre = document.getElementById("NombreAlarma").value
    
    ListaDeAlarmas.push({ Alarma, AlarmaNombre })
    localStorage.setItem('alarmas', JSON.stringify(ListaDeAlarmas))

    SonarAlarma(Alarma, AlarmaNombre)
    }
  }
const miAudio = new Audio('/Audio/Alarma.mp3');
function SonarAlarma(Alarma, AlarmaNombre) {
    let HoraActual = new Date()
    let TiempoRestante = (Alarma - HoraActual)
    
    if (TiempoRestante > 0) {
      setTimeout(() => {
        miAudio.play();
      }, TiempoRestante-12000)
      setTimeout(() => {
        alert(AlarmaNombre)
      }, TiempoRestante)
    }
}
// BUSCADOR 
document.getElementById('mostrarBuscar').addEventListener('click', () => {
  const buscadorContainer = document.getElementById('buscadorContainer');
  buscadorContainer.style.display = buscadorContainer.style.display === 'none' ? 'block' : 'none';
  mostrarListaAlarmas();
});

function mostrarListaAlarmas() {
  const lista = document.getElementById('lista');
  lista.innerHTML = '';
  const alarmasGuardadas = JSON.parse(localStorage.getItem('alarmas')) || [];
  ListaDeAlarmas = alarmasGuardadas;

  alarmasGuardadas.forEach((alarma) => {
    const li = document.createElement('li');
    li.textContent = alarma.AlarmaNombre;
    li.style.cursor = 'pointer';

    li.addEventListener('click', () => {
      mostrarInfoAlarma(alarma);
    });

    lista.appendChild(li);
  });
}

document.getElementById('buscador').addEventListener('input', (e) => {
  const texto = e.target.value.toLowerCase();
  const lista = document.getElementById('lista');
  lista.innerHTML = '';

  const alarmasFiltradas = ListaDeAlarmas.filter(alarma =>
    alarma.AlarmaNombre.toLowerCase().includes(texto)
  );

  alarmasFiltradas.forEach((alarma) => {
    const li = document.createElement('li');
    li.textContent = alarma.AlarmaNombre;
    li.style.cursor = 'pointer';
    li.addEventListener('click', () => {
      mostrarInfoAlarma(alarma);
    });
    lista.appendChild(li);
  });
});

// 
function mostrarInfoAlarma(alarma) {
  const infoBox = document.getElementById('infoBox');

  const fechaAlarma = new Date(alarma.Alarma);
  const ahora = new Date();
  let diferencia = fechaAlarma - ahora;

  // Calcular tiempo inicial
  const horas = Math.floor(diferencia / (1000 * 60 * 60));
  const minutos = Math.floor((diferencia % (1000 * 60 * 60)) / (1000 * 60));
  const segundos = Math.floor((diferencia % (1000 * 60)) / 1000);

  infoBox.innerHTML = `
    <h3>${alarma.AlarmaNombre}</h3>
    <p><strong>Fecha y hora:</strong> ${fechaAlarma.toLocaleString()}</p>
    <p class="tiempo-restante"><strong>Tiempo restante:</strong> ${horas}h ${minutos}m ${segundos}s</p>
  `;
  infoBox.style.display = 'block';
  infoBox.style.backgroundColor = '#ffffff';

  // Detener contador anterior (si había)
  clearInterval(infoBox.timer);

  // Iniciar actualización cada segundo
  infoBox.timer = setInterval(() => {
    const ahora = new Date();
    let diff = fechaAlarma - ahora;

    const tiempoElem = infoBox.querySelector('.tiempo-restante');

    if (diff <= 0) {
      clearInterval(infoBox.timer);
      tiempoElem.innerHTML = `<strong>⏰ La alarma ya sonó</strong>`;
      infoBox.style.backgroundColor = '#ffe6e6';
      return;
    }

    const h = Math.floor(diff / (1000 * 60 * 60));
    const m = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
    const s = Math.floor((diff % (1000 * 60)) / 1000);

    tiempoElem.innerHTML = `<strong>Tiempo restante:</strong> ${h}h ${m}m ${s}s`;
  }, 1000);
}
