import { Inject, ViewEncapsulation, Component, ChangeDetectionStrategy, ViewChild, OnDestroy, OnInit, ChangeDetectorRef, TemplateRef } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { startOfDay, endOfDay, isSameDay, isSameMonth } from 'date-fns';
import { Subject } from 'rxjs';
import { RRule } from 'rrule';
import { CalendarEvent, CalendarWeekViewBeforeRenderEvent, CalendarMonthViewBeforeRenderEvent, CalendarDayViewBeforeRenderEvent, CalendarEventAction, CalendarEventTimesChangedEvent, CalendarView } from 'angular-calendar';
import { MatDialog } from '@angular/material/dialog';
import { BaseComponent } from '../../base/base.component';
import { CalendarService } from './calendar.service'

export interface DialogData {
  action: string;
  event: CalendarEvent;
}

interface RecurringEvent {
  title: string;
  color: any;
  rrule?: {
    freq: any;
    bymonth?: number;
    bymonthday?: number;
    byweekday?: any;
  };
}

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3',
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF',
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA',
  },
};

@Component({
  selector: 'calendar-component',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrls: ['calendar.component.scss'],
  templateUrl: 'calendar.component.html',
  providers: [CalendarService],
  encapsulation: ViewEncapsulation.None,
})
export class CalendarComponent extends BaseComponent implements OnInit, OnDestroy {
  @ViewChild('modalContent', { static: true }) modalContent: TemplateRef<any>;
  private readonly darkThemeClass = 'dark-theme';
  view: CalendarView = CalendarView.Month;
  courses: any[];

  CalendarView = CalendarView;

  viewDate: Date = new Date();
  constructor(private cdr: ChangeDetectorRef, private readonly _calendarService: CalendarService, @Inject(DOCUMENT) private document, private modal: MatDialog) {
    super();
  }

  ngOnInit(): void {
    this.document.body.classList.add(this.darkThemeClass);
    this._calendarService.getCourses().then(response => {
      this.courses = response;
      
      for (var i = 0; i < this.courses.length; i++) {
        // this.addEvent(this.courses[i]);
        this.updateCalendarEvents(this.courses[i]);
      }
    });
  }

  ngOnDestroy(): void {
    this.document.body.classList.remove(this.darkThemeClass);
  }

  actions: CalendarEventAction[] = [
    {
      label: '<i class="fas fa-fw fa-pencil-alt"></i>',
      a11yLabel: 'Edit',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Edited', event);
      },
    },
    {
      label: '<i class="fas fa-fw fa-trash-alt"></i>',
      a11yLabel: 'Delete',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.events = this.events.filter((iEvent) => iEvent !== event);
        this.handleEvent('Deleted', event);
      },
    },
  ];

  refresh: Subject<any> = new Subject();
  events: CalendarEvent[] = [];
  recurringEvents: RecurringEvent[] = [
    {
      title: 'Recurs weekly on mondays',
      color: colors.red,
      rrule: {
        freq: RRule.WEEKLY,
        byweekday: [RRule.MO],
      },
    },
  ];
  activeDayIsOpen: boolean = true;

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd,
  }: CalendarEventTimesChangedEvent): void {
    this.events = this.events.map((iEvent) => {
      if (iEvent === event) {
        return {
          ...event,
          start: newStart,
          end: newEnd,
        };
      }
      return iEvent;
    });
    this.handleEvent('Dropped or resized', event);
  }

  handleEvent(action: string, event: CalendarEvent): void {
    const modal = this.modal.open(this.modalContent, { width: '250px', data: { action: action, event: event } });
    modal.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  addEvent(course): void {
    this.events = [
      ...this.events,
      {
        title: course.name,
        start: startOfDay(new Date(course.startDate)),
        end: endOfDay(new Date(course.startDate)),
        color: colors.red,
        draggable: true,
        resizable: {
          beforeStart: true,
          afterEnd: true,
        },
      },
    ];
  }

  updateCalendarEvents(course): void {{
      this.recurringEvents.forEach((event) => {
        var endDate = new Date(course.startDate);
        endDate.setMonth(endDate.getMonth() + 4);
        const rule: RRule = new RRule({
          ...event.rrule,
          dtstart: startOfDay(new Date(course.startDate)),
          until: endOfDay(endDate)
        });
        const { title, color } = event;

        rule.all().forEach((date) => {
          this.events.push({
            title,
            color,
            start: startOfDay(new Date(date)),
          });
        });
      });
      this.cdr.detectChanges();
    }
  }

  deleteEvent(eventToDelete: CalendarEvent) {
    this.events = this.events.filter((event) => event !== eventToDelete);
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
